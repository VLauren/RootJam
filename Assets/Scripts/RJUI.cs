using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RJUI : MonoBehaviour
{
    public Slider scoreSlider;

    public Text gameScore;

    public Text gameTime;

    // Start is called before the first frame update
    void Start()
    {
        scoreSlider = GetComponent<Slider>();

        scoreSlider.value = 0;

        gameScore.text = "pruebita";
    }

    // Update is called once per frame
    void Update()
    {
        scoreSlider.value = (float)RJGame.growthPoints / (float)RJGame.level3BreakPoint;
        gameTime.text = (int)RJPlanetSpawner.RemainingTime + " ";

        gameScore.text = RJPlanetSpawner.gameScore + "";
    }
}
