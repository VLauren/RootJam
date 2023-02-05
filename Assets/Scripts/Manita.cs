using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manita : MonoBehaviour
{
    void Start()
    {
        GetComponent<Image>().enabled = false;
    }

    void Update()
    {
        GetComponent<Image>().enabled = (RJChar.Instance.currentResource != null && RJChar.Instance.canMove);
    }
}
