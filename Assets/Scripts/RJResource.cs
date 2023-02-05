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

    void Update()
    {
        bool show = RJChar.currentResource == this && !RJChar.Instance.canMove;
        foreach (Transform child in transform.GetChild(0))
        {
            child.gameObject.SetActive(show);
        }
    }
}
