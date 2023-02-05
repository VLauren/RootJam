using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RJCanvas : MonoBehaviour
{

    public GameObject smashSliderFill;
    public GameObject smashSliderFrame;
    // Start is called before the first frame update
    void Start()
    {
        // smashSliderFill = transform.Find("SliderMachaca").gameObject;
        // smashSliderFrame = transform.Find("PresionaBoton").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (RJChar.canGather)
        {
            smashSliderFill.SetActive(true);
            smashSliderFrame.SetActive(true);
        }
        else
        {
            smashSliderFill.SetActive(false);
            smashSliderFrame.SetActive(false);
        }
    }
}
