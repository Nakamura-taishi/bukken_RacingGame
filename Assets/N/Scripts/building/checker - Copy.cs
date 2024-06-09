using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class inputDemo : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        //L Stick
        float lsh = Input.GetAxis("L_stick_H");
        float lsv = Input.GetAxis("L_stick_V");
        if ((lsh != 0) || (lsv != 0))
        {
            Debug.Log("L stick:" + lsh + "," + lsv);
        }
        //R Stick
        float rsh = Input.GetAxis("R_stick_H");
        float rsv = Input.GetAxis("R_stick_V");
        if ((rsh != 0) || (rsv != 0))
        {
            Debug.Log("R stick:" + rsh + "," + rsv);
        }
        /*
                //Trigger
                float tri = Input.GetAxis("L_R_Trigger");
                if (tri > 0)
                {
                    Debug.Log("L trigger:" + tri);
                }
                else if (tri < 0)
                {
                    Debug.Log("R trigger:" + tri);
                }
                else
                {
                    Debug.Log("  trigger:none");
                }
        */
        float zl = Input.GetAxis("ZL");
        Debug.Log(zl);
        /*
        float r = /*
        float r = Input.GetAxis("R");
        float l = Input.GetAxis("L");
        Debug.Log("R :" + r);
        Debug.Input.GetAxis("R");
        float l = Input.GetAxis("L");
        Debug.Log("R :" + r);
        Debug.Log("L :" + l);
        */
    }
}