using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RJCam : MonoBehaviour
{
    public Vector3 Offset;
    public float Angle;

    [Space()]
    public float SmoothTime;

    Transform Target;
    Vector3 DampVelocity;

    void Start()
    {
        Target = FindObjectOfType<RJChar>().transform;
    }

    void LateUpdate()
    {
        Vector3 targetPosition = Target.position + transform.right * Offset.x + transform.up * Offset.y + transform.forward * Offset.z;
        // Quaternion targetRotation = Target.rotation * Quaternion.Euler(Angle, 0, 0);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref DampVelocity, 0.1f);
        // transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 360);
        transform.rotation = Target.rotation * Quaternion.Euler(Angle, 0, 0);
    }
}
