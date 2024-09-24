using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtils
{
    public static Vector3 ForceY(Vector3 baseVector, Vector3 yVector)
    {
        return new Vector3(baseVector.x, yVector.y, baseVector.z);
    }
    public static Vector3 ForceY(Vector3 baseVector, float yPosition)
    {
        return new Vector3(baseVector.x, yPosition, baseVector.z);
    }
}
