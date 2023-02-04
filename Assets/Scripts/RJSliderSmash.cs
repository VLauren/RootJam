using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RJSliderSmash : MonoBehaviour
{
    public Slider sliderSmash;

    // Start is called before the first frame update
    void Start()
    {
        sliderSmash = GetComponent<Slider>();

        sliderSmash.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        sliderSmash.value = (float)RJGame.currentGatherPoints / (float)RJGame.resource1Size;

    }
}
