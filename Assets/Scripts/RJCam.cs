using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RJCam : MonoBehaviour
{
    public static RJCam Instance { get; private set; }

    public Vector3 Offset;
    public float Angle;

    [Space()]
    public float SmoothTime;

    public bool MovementActive = true;

    Transform Target;
    Vector3 DampVelocity;
    float Vel;
    float TargetY;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Target = FindObjectOfType<RJChar>().transform;
    }

    void LateUpdate()
    {
        /*
        Vector3 targetPosition = Target.position + transform.right * Offset.x + transform.up * Offset.y + transform.forward * Offset.z;
        // Quaternion targetRotation = Target.rotation * Quaternion.Euler(Angle, 0, 0);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref DampVelocity, 0.1f);
        // transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 360);
        transform.rotation = Target.rotation * Quaternion.Euler(Angle, 0, 0);
        */

        if (MovementActive)
            TargetY -= Time.deltaTime * 16 * Input.GetAxisRaw("Horizontal");

        // float newRotY = Mathf.SmoothDamp(transform.parent.rotation.eulerAngles.y, TargetY, ref Vel, 0.1f);
        float newRotY = TargetY;

        transform.parent.rotation = Quaternion.Euler(
            transform.parent.rotation.eulerAngles.x,
            newRotY,
            transform.parent.rotation.eulerAngles.z);
    }
}
