using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t_AllWheel : MonoBehaviour
{
    public wheel[] Wheels;
    
    JointSpring SSJo;
    WheelFrictionCurve FFWF;
    WheelFrictionCurve SFWF;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {        for (int i = 0; i<Wheels.Length;i++)
        {
            Wheels[i].right.mass = Wheels[i].Mass;
            Wheels[i].right.radius = Wheels[i].Radius;
            Wheels[i].right.wheelDampingRate = Wheels[i].WDR;
            Wheels[i].right.suspensionDistance = Wheels[i].SD;
            Wheels[i].right.forceAppPointDistance = Wheels[i].FAPD;
            Wheels[i].right.center = Wheels[i].Center;
            SSJo.spring = Wheels[i].Sups.Spring;
            SSJo.damper = Wheels[i].Sups.Damper;
            SSJo.targetPosition = Wheels[i].Sups.Targetposition;
            FFWF.extremumSlip = Wheels[i].Frif.ExSlip;
            FFWF.extremumValue = Wheels[i].Frif.ExValue;
            FFWF.asymptoteSlip = Wheels[i].Frif.AsSlip;
            FFWF.asymptoteValue = Wheels[i].Frif.AsValue;
            FFWF.stiffness = Wheels[i].Frif.Stiffness;
            SFWF.extremumSlip = Wheels[i].Sidf.ExSlip;
            SFWF.extremumValue = Wheels[i].Sidf.ExValue;
            SFWF.asymptoteSlip = Wheels[i].Sidf.AsSlip;
            SFWF.asymptoteValue = Wheels[i].Sidf.AsValue;
            SFWF.stiffness = Wheels[i].Sidf.Stiffness;
            Wheels[i].right.suspensionSpring = SSJo;
            Wheels[i].right.forwardFriction = FFWF;
            Wheels[i].right.sidewaysFriction = SFWF;
            Wheels[i].left.mass = Wheels[i].Mass;
            Wheels[i].left.radius = Wheels[i].Radius;
            Wheels[i].left.wheelDampingRate = Wheels[i].WDR;
            Wheels[i].left.suspensionDistance = Wheels[i].SD;
            Wheels[i].left.forceAppPointDistance = Wheels[i].FAPD;
            Wheels[i].left.center = Wheels[i].Center;
            Wheels[i].left.suspensionSpring = SSJo;
            Wheels[i].left.forwardFriction = FFWF;
            Wheels[i].left.sidewaysFriction = SFWF;
        }
    }
    
    
}
[System.Serializable]
public class wheel
{
    public WheelCollider right;
    public WheelCollider left;
    public float Mass = 20f;
    public float Radius = 0.4f;
    public float WDR = 0.25f;
    public float SD = 0.5f;
    public float FAPD = 0.44f;
    public Vector3 Center = new Vector3(0f, 0.15f, 0f);
    public SS Sups;
    public FF Frif;
    public SF Sidf;
}

[System.Serializable]
public class SS
{

    public float Spring = 40000f;
    public float Damper = 5000f;
    public float Targetposition = 0.5f;
    
}
[System.Serializable]
public class FF
{
    public float ExSlip = 1f;
    public float ExValue = 1f;
    public float AsSlip = 0.8f;
    public float AsValue = 0.5f;
    public float Stiffness = 1f;
}
[System.Serializable]
public class SF
{
    public float ExSlip = 1f;
    public float ExValue = 1f;
    public float AsSlip = 0.5f;
    public float AsValue = 0.75f;
    public float Stiffness = 1f;
}