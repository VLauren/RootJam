using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RJResource : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
