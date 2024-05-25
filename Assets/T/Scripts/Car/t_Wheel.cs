using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class t_Wheel : MonoBehaviour
{
    public GameObject car;
    public WheelCollider col;
    // Start is called before the first frame update
    float steer = 0;

    // Update is called once per frame
    void Update()
    {
        t_Runner motor = car.GetComponent<t_Runner>();
        Rigidbody rg2 = car.GetComponent<Rigidbody>();
        float vo = rg2.velocity.magnitude;
        if (vo < 1)
        {
            vo = 0;
        }
        float stter1 = steer;
        steer = motor.steering1;
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, col.steerAngle - transform.localEulerAngles.z, transform.localEulerAngles.z);
        
        
        float direction = Input.GetAxis("Vertical");
        if (direction == 0)
        {
            transform.Rotate(col.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.Rotate(col.rpm / 60 * 360 * Time.deltaTime + (direction) * (motor.maxMotorTorque/50), 0, 0);
        }
    }
}
