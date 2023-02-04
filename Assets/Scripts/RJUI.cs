using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RJUI : MonoBehaviour
{
    Slider scoreSlider;

    // Start is called before the first frame update
    void Start()
    {
        scoreSlider = GetComponent<Slider>();

        scoreSlider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreSlider.value = (float)RJGame.growthPoints / (float)RJGame.level3BreakPoint;
    }
}
