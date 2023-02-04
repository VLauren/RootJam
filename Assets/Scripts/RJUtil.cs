using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RJUtil
{
    public static void SphereMove(Transform transformToMove, Vector3 movement)
    {
        MovementDummy.Instance.transform.position = transformToMove.position;
        MovementDummy.Instance.transform.rotation = transformToMove.rotation;

        // Magia, no pregunteis
        MovementDummy.Instance.transform.parent.rotation = Quaternion.Euler(
            // Mathf.Clamp(
                MovementDummy.Instance.transform.parent.rotation.eulerAngles.x - movement.x,
                // 290, 304),
            MovementDummy.Instance.transform.parent.rotation.eulerAngles.y - movement.y,
            MovementDummy.Instance.transform.parent.rotation.eulerAngles.z);

        Debug.Log(MovementDummy.Instance.transform.parent.rotation.eulerAngles.x);

        transformToMove.position = MovementDummy.Instance.transform.position;
        transformToMove.rotation = MovementDummy.Instance.transform.rotation;
    }
}
