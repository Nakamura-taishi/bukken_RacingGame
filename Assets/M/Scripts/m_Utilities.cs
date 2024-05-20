using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class m_Utilities
{
    public static float GetAngle(Vector3 nextPosition, Transform carTransform)
    {
        Vector3 bectle = nextPosition - carTransform.position;
        Vector3 axis = Vector3.Cross(carTransform.forward,bectle);
        float angle = Vector3.Angle(carTransform.forward,bectle);
        return angle * (axis.y > 0 ? 1 : -1);
    }
}
