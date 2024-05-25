using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class t_NeedleRotation : MonoBehaviour
{
    public t_NeedleRotation needleRotationScript;
    double speed;
    int speed_angle;
    int processed_speed_angle;
    public t_SpeedScript speedScript;

    void Update()
    {
        Transform needleTransform = needleRotationScript.transform;
        
        Vector3 needleAngle = needleTransform.eulerAngles;
        speed = speedScript.speed;
        speed_angle = (int)(270 * speed / 225);
        if(speed_angle >= 270)
        {
            processed_speed_angle = 270;
            needleAngle.z = -1 * processed_speed_angle + 45;
        }
        else
        {
            needleAngle.z = -1 * speed_angle + 45;
        }
        needleTransform.eulerAngles = needleAngle;
    }
}
