using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run1 : MonoBehaviour {

    public WheelCollider flwheelCollider;
    public WheelCollider frwheelCollider;
    public WheelCollider rlwheelCollider;
    public WheelCollider rrwheelCollider;

    public Transform flwheelM;
    public Transform frwheelM;
    public Transform rlwheelM;
    public Transform rrwheelM;

    public Transform fl;
    public Transform fr;

    public float max = 140;
    public float min = 30;
    public float speed = 1000;
    private int is_peng = 1;

    public float brakeTorque = 100;
    public bool isBreaking;

    public float motorTorque = 2000;
    public float motorTorque1 = 2000;
    public Transform centerOfMass;

    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass.localPosition;
    }

    // Update is called once per frame
    void Update() {
        float currentSpeed = flwheelCollider.rpm * (flwheelCollider.radius * 2 * Mathf.PI) * 60 / 1000;


        if (currentSpeed > 0 && Input.GetAxis("Vertical1") < 0 || currentSpeed < 0 && Input.GetAxis("Vertical1") > 0)
        {
            isBreaking = true;
        }
        else {
            isBreaking = false;
        }

        if (currentSpeed > max && Input.GetAxis("Vertical1") > 0 || currentSpeed < -min && Input.GetAxis("Vertical1") < 0)
        {
            flwheelCollider.motorTorque = 0;
            frwheelCollider.motorTorque = 0;
        }
        else if (is_peng == 0)
        {
            flwheelCollider.motorTorque = 0;
            frwheelCollider.motorTorque = 0;
        }
        else
        {
            flwheelCollider.motorTorque = speed;
            frwheelCollider.motorTorque = speed;
            /*flwheelCollider.motorTorque = Input.GetAxis("Vertical1") * motorTorque;
            frwheelCollider.motorTorque = Input.GetAxis("Vertical1") * motorTorque;*/
        }

        if (isBreaking)
        {
            flwheelCollider.motorTorque = 0;
            frwheelCollider.motorTorque = 0;
            flwheelCollider.brakeTorque = brakeTorque;
            frwheelCollider.brakeTorque = brakeTorque;
        }
        else
        {
            flwheelCollider.brakeTorque = 0;
            frwheelCollider.brakeTorque = 0;
        }
        flwheelCollider.steerAngle = Input.GetAxis("Horizontal1") * motorTorque1;
        frwheelCollider.steerAngle = Input.GetAxis("Horizontal1") * motorTorque1;


        RotateWheel();
        StreerWheel();
    }
    void RotateWheel()
    {
        fl.Rotate(flwheelCollider.rpm * 6 * Time.deltaTime * Vector3.right);
        fr.Rotate(flwheelCollider.rpm * 6 * Time.deltaTime * Vector3.right);
        rlwheelM.Rotate(flwheelCollider.rpm * 6 * Time.deltaTime * Vector3.right);
        rrwheelM.Rotate(flwheelCollider.rpm * 6 * Time.deltaTime * Vector3.right);
    }
    void StreerWheel()
    {
        Vector3 localEulerAngles = flwheelM.localEulerAngles;
        localEulerAngles.y = flwheelCollider.steerAngle;

        flwheelM.localEulerAngles = localEulerAngles;
        frwheelM.localEulerAngles = localEulerAngles;
    }
    void OnTriggerEnter(Collider hit)
    {
        is_peng = 0;
    }
}
