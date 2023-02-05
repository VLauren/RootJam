using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPressIndic : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(SwitchGraphs());
    }

    IEnumerator SwitchGraphs()
    {
        while (true)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);

            yield return new WaitForSeconds(0.1f);

            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
