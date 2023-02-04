using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RJResource : MonoBehaviour
{

    public int resourceLevel;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == RJChar.Instance.gameObject)
        {

            print("resourceLevel de la cosa" + resourceLevel);
            print("currentLevel de la cosa" + RJChar.Instance.CurrentLevel);

            if (RJChar.Instance.CurrentLevel >= resourceLevel)
            {
                RJChar.currentResource = this;
            }

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
