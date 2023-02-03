using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RJCam : MonoBehaviour
{
    public Vector3 Offset;
    public float Angle;

    Transform Target;

    void Start()
    {
        Target = FindObjectOfType<RJChar>().transform;
    }

    void Update()
    {
        transform.rotation = Target.rotation * Quaternion.Euler(Angle, 0, 0);

        transform.position = Target.position + transform.right * Offset.x + transform.up * Offset.y + transform.forward * Offset.z;
    }
}
