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

    float GoblinSpawnRate = 0.2f;

    Material StartMaterial;

    void Awake()
    {
        Instance = this;
        StartMaterial = GetComponent<Renderer>().material;
    }

    private void Start()
    {
        SpawnPlanetElements();

        InvokeRepeating("SpawnGoblin", 3, 1f / GoblinSpawnRate);

        RJAudio.AudioSource.SetIntVar("musicvar", 0);
        RJAudio.AudioSource.Play("musica");
    }

    public int DebugGoblinsToSpawn;
    public void SpawnPlanetElements()
    {
        for (int i = 0; i < DebugGoblinsToSpawn; i++)
            Instantiate(GoblinPrefab, Vector3.zero, Quaternion.Euler(Random.Range(-70, -55), Random.Range(-180, 180), 0));

        for (int i = 0; i < 15; i++)
            Instantiate(Resource1Prefab, Vector3.zero, Quaternion.Euler(Random.Range(-70, -55), Random.Range(-180, 180), 0));
    }

    public static void ChangeMaterial(bool transparent)
    {
        return; // Ya veremos

        if (transparent)
            Instance.GetComponent<Renderer>().material = Instance.TransparentMaterial;
        else
            Instance.GetComponent<Renderer>().material = Instance.StartMaterial;
    }

    void SpawnGoblin()
    {
        float yRot = RJChar.Instance.transform.parent.eulerAngles.y;
        Instantiate(GoblinPrefab, Vector3.zero, Quaternion.Euler(Random.Range(-70, -55), Random.Range(yRot - 20, yRot + 20), 0));
    }
}
