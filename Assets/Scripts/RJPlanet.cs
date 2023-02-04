using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RJPlanet : MonoBehaviour
{
    public static RJPlanet Instance { get; private set; }
    public Material TransparentMaterial;

    [Header("Prefabs")]
    public GameObject GoblinPrefab;
    public GameObject Resource1Prefab;

    Material StartMaterial;

    void Awake()
    {
        Instance = this;
        StartMaterial = GetComponent<Renderer>().material;
    }

    private void Start()
    {
        SpawnPlanetElements();
    }

    public void SpawnPlanetElements()
    {
        for (int i = 0; i < 3; i++)
            Instantiate(GoblinPrefab, Vector3.zero, Quaternion.Euler(Random.Range(-70, -54), Random.Range(-180, 180), 0));

        for (int i = 0; i < 20; i++)
            Instantiate(Resource1Prefab, Vector3.zero, Quaternion.Euler(Random.Range(-70, -54), Random.Range(-180, 180), 0));
    }

    public static void ChangeMaterial(bool transparent)
    {
        if(transparent)
            Instance.GetComponent<Renderer>().material = Instance.TransparentMaterial;
        else
            Instance.GetComponent<Renderer>().material = Instance.StartMaterial;
    }
}
