using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RJResource : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == RJChar.Instance.gameObject)
        {

            RJChar.currentResource = this;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject == RJChar.Instance.gameObject)
        {

            RJChar.currentResource = null;
        }
    }
}
