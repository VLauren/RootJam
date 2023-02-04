using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RJPlanet : MonoBehaviour
{
    public static RJPlanet Instance { get; private set; }
    public Material TransparentMaterial;

    Material StartMaterial;

    void Awake()
    {
        Instance = this;
        StartMaterial = GetComponent<Renderer>().material;
    }

    public static void ChangeMaterial(bool transparent)
    {
        if(transparent)
            Instance.GetComponent<Renderer>().material = Instance.TransparentMaterial;
        else
            Instance.GetComponent<Renderer>().material = Instance.StartMaterial;
    }
}
