using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RJGameOver : MonoBehaviour
{
    public Text finalScore;

    bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        finalScore.text = "Final Score: " + RJPlanetSpawner.gameScore;

        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("TestScene2", LoadSceneMode.Single);
            RJPlanetSpawner.gameScore = 0;
        }

    }
}
