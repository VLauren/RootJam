using KrillAudio.Krilloud;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RJGameOver : MonoBehaviour
{
    public Text finalScore;

    bool gameOver = false;

    KLAudioSource AudioSource;

    void Start()
    {
        AudioSource = GetComponent<KLAudioSource>();
        AudioSource.SetIntVar("musicvar", 2);
        AudioSource.Play("musica");
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
