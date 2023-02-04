using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RJCam : MonoBehaviour
{
    public static RJCam Instance { get; private set; }

    public float SmoothTime = 0.1f;

    [Space()]
    public float Fase1MovingAngle = -121;
    public float Fase1MovingTilt = -21;
    public float Fase1MovingDistance = 45;
    public float Fase1RootedAngle = -121;
    public float Fase1RootedTilt = -21;
    public float Fase1RootedDistance = 45;

    [Space()]
    public float Fase2MovingAngle;
    public float Fase2MovingTilt;
    public float Fase2MovingDistance;
    public float Fase2RootedAngle;
    public float Fase2RootedTilt;
    public float Fase2RootedDistance;

    [Space()]
    public float Fase3MovingAngle;
    public float Fase3MovingTilt;
    public float Fase3MovingDistance;
    public float Fase3RootedAngle;
    public float Fase3RootedTilt;
    public float Fase3RootedDistance;

    [HideInInspector]
    public bool MovementActive = true;

    Vector3 smoothVel;
    Vector3 parentSmoothVel;
    float distSmoothVel;

    void Awake()
    {
        Instance = this;
    }

    void LateUpdate()
    {
        float newParentRotY = RJChar.Instance.transform.parent.eulerAngles.y;

        Vector3 newParentEuler = new Vector3(RJChar.Instance.canMove ? Fase1MovingAngle : Fase1RootedAngle, newParentRotY, 0);
        Vector3 newLocalEulerAngles = new Vector3(RJChar.Instance.canMove ? Fase1MovingTilt : Fase1RootedTilt, -180, 0);
        float newDistance = RJChar.Instance.canMove ? Fase1MovingDistance : Fase1RootedDistance;

        if (RJChar.Instance.CurrentLevel == 1)
        {
            newParentEuler = new Vector3(RJChar.Instance.canMove ? Fase2MovingAngle : Fase2RootedAngle, newParentRotY, 0);
            newLocalEulerAngles = new Vector3(RJChar.Instance.canMove ? Fase2MovingTilt : Fase2RootedTilt, -180, 0);
            newDistance = RJChar.Instance.canMove ? Fase2MovingDistance : Fase2RootedDistance;
        }
        else if (RJChar.Instance.CurrentLevel == 2)
        {
            newParentEuler = new Vector3(RJChar.Instance.canMove ? Fase3MovingAngle : Fase3RootedAngle, newParentRotY, 0);
            newLocalEulerAngles = new Vector3(RJChar.Instance.canMove ? Fase3MovingTilt : Fase3RootedTilt, -180, 0);
            newDistance = RJChar.Instance.canMove ? Fase3MovingDistance : Fase3RootedDistance;
        }


        // transform.parent.eulerAngles = newParentEuler;
        // transform.parent.rotation = Quaternion.Euler(newParentEuler);
        // transform.parent.rotation = Quaternion.RotateTowards(Quaternion.Euler(transform.parent.eulerAngles), Quaternion.Euler(newParentEuler), Time.deltaTime * 90);
        // transform.parent.eulerAngles = Vector3.SmoothDamp(transform.parent.eulerAngles, newParentEuler, ref parentSmoothVel, 0.1f);
        transform.parent.rotation = RJUtil.SmoothDampQuaternion(Quaternion.Euler(transform.parent.eulerAngles), Quaternion.Euler(newParentEuler), ref parentSmoothVel, SmoothTime);

        // transform.localEulerAngles = newLocalEulerAngles;
        // transform.localRotation = Quaternion.RotateTowards(Quaternion.Euler(transform.localEulerAngles), Quaternion.Euler(newLocalEulerAngles), Time.deltaTime * 90);
        // transform.localEulerAngles = Vector3.SmoothDamp(transform.localEulerAngles, newLocalEulerAngles, ref smoothVel, 0.1f);
        transform.localRotation = RJUtil.SmoothDampQuaternion(Quaternion.Euler(transform.localEulerAngles), Quaternion.Euler(newLocalEulerAngles), ref smoothVel, SmoothTime);

        // transform.localPosition = new Vector3(0, 0, newDistance);
        transform.localPosition = new Vector3(0, 0, Mathf.SmoothDamp(transform.localPosition.z, newDistance, ref distSmoothVel, SmoothTime));
    }
}
