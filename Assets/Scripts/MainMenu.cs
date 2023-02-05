using KrillAudio.Krilloud;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public KLAudioSource AudioSource;
    void Start()
    {
        AudioSource = GetComponent<KLAudioSource>();
        AudioSource.SetIntVar("musicvar", 0);
        AudioSource.Play("musica");
        RJPlanetSpawner.gameScore = 0;
    }

    void Update()
    {
        if(Input.anyKeyDown)
        {
            SceneManager.LoadScene("TestScene2");
        }
    }
}
