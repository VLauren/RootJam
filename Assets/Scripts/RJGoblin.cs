using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RJGoblin : MonoBehaviour
{
    public int HVelocity = 8;
    public int VVelocity = 8;

    void Update()
    {
        Vector3 dir = transform.InverseTransformPoint(RJChar.Instance.transform.position);
        dir.y = 0;
        dir.Normalize();

        // Movimiento
        RJUtil.SphereMove(transform, new Vector3(
        Time.deltaTime * VVelocity * dir.z,
        - Time.deltaTime * HVelocity * dir.x,
        0));
        
    }
}
