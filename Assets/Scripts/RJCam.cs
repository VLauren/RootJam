using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RJCam : MonoBehaviour
{
    public static RJCam Instance { get; private set; }

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

    void Awake()
    {
        Instance = this;
    }

    void LateUpdate()
    {
        float newParentRotY = RJChar.Instance.transform.parent.eulerAngles.y;

        Vector3 newParentEuler = new Vector3(
            RJChar.Instance.canMove ? Fase1MovingAngle : Fase1RootedAngle,
            newParentRotY,
            0);

        Vector3 newLocalEulerAngles = new Vector3(RJChar.Instance.canMove ? Fase1MovingTilt : Fase1RootedTilt, -180, 0);

        float newDistance = RJChar.Instance.canMove ? Fase1MovingDistance : Fase1RootedDistance;

        transform.parent.eulerAngles = newParentEuler;
        transform.localEulerAngles = newLocalEulerAngles;
        transform.localPosition = new Vector3(0, 0, newDistance);
    }
}
