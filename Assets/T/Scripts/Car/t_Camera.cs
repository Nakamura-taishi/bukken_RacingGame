using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class t_Camera : MonoBehaviour
{
    public GameObject target; //�ǂ�������^�[�Q�b�g
    
    public bool isVPressedNow;

    void Start()
    {
        if (!target)
        {
            target = GameObject.FindWithTag("CAR");
        }
    }

    void LateUpdate()
    {
        isVPressedNow = Keyboard.current.vKey.isPressed;

        t_Runner cam = target.GetComponent<t_Runner>();
        if ((isVPressedNow || Input.GetAxis("ZL") != 0) && cam.falltimer == 0)
        {
            if (cam.steering1 > 0)
            {
                if (transform.localRotation.y * 100 > -cam.SteeringAngle)
                {
                    transform.Rotate(0f, -0.4f, 0f);

                    Debug.Log(transform.localRotation.y);
                    if (transform.localPosition.x < 4)
                    {
                        transform.Translate(0.04f, 0f, 0f);
                    }
                }
            }
            else if (cam.steering1 < 0)
            {
                if (transform.localRotation.y * 100 < cam.SteeringAngle)
                {
                    transform.Rotate(0f, 0.4f, 0f);
                }
                if (transform.localPosition.x > -4)
                {
                    transform.Translate(-0.04f, 0f, 0f);
                }
            }
        }
        else if (transform.localRotation.y * 100 < -0.10)
        {
            transform.Rotate(0f, 0.4f, 0f);
        }
        else if (transform.localRotation.y * 100 > 0.10)
        {
            transform.Rotate(0f, -0.4f, 0f);
        }


        if ((isVPressedNow || Input.GetAxis("ZL") != 0) && cam.falltimer == 0)
        {
            if (cam.NowEngine >= 1)
            {
                if (transform.localPosition.z > -8)
                {
                    transform.Translate(0f, 0f, -0.2f);
                }
            }
        }
        else if (transform.localPosition.z < -6 && !(isVPressedNow || Input.GetAxis("ZL") != 0))
        {
            transform.Translate(0f, 0f, 0.2f);
        }
        else if (transform.localPosition.z > -5.8f && !(isVPressedNow || Input.GetAxis("ZL") != 0))
        {
            transform.Translate(0f, 0f, -0.2f);
        }
        if (transform.localPosition.x < 0 && !(isVPressedNow || Input.GetAxis("ZL") != 0))
        {
            transform.Translate(0.04f, 0f, 0f);
        }
        if (transform.localPosition.x > 0 && !(isVPressedNow || Input.GetAxis("ZL") != 0))
        {
            transform.Translate(-0.04f, 0f, 0f);
        }
    }
}
