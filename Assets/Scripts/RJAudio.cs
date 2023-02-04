using KrillAudio.Krilloud;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RJAudio : MonoBehaviour
{
    public static KLAudioSource AudioSource { get; private set; }

    void Awake()
    {
        AudioSource = GetComponent<KLAudioSource>();
    }

    private void Start()
    {
        AudioSource.SetIntVar("musicvar", 0);
        AudioSource.Play("musica");

        print("sonido lanzado");
    }
}
