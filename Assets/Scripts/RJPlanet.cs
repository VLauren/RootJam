using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RJPlanet : MonoBehaviour
{
    public static RJPlanet Instance { get; private set; }
    public Material TransparentMaterial;

    [Header("Prefabs")]
    public GameObject GoblinPrefab;
    public GameObject Resource1Prefab;
    public GameObject Resource2Prefab;
    public GameObject Resource3Prefab;

    [Space()]
    public Color BackgroundColor;

    public float GoblinSpawnRate = 0.2f;

    Material StartMaterial;

    //lo del tiempo
    //public GameObject timeDisplay;
    // int seconds = 50;
    // bool deductingTime;





    void Awake()
    {
        Instance = this;
        StartMaterial = GetComponent<Renderer>().material;
    }

    private void Start()
    {
        SpawnPlanetElements();

        InvokeRepeating("SpawnGoblin", 3, 1f / GoblinSpawnRate);


    }

    private void Update()
    {

    }

    public int DebugGoblinsToSpawn;
    public void SpawnPlanetElements()
    {
        for (int i = 0; i < DebugGoblinsToSpawn; i++)
            Instantiate(GoblinPrefab, Vector3.zero, Quaternion.Euler(Random.Range(-70, -55), Random.Range(-180, 180), 0));

        // Spawn recursos
        for (int i = 0; i < 10; i++)
            Instantiate(Resource1Prefab, Vector3.zero, Quaternion.Euler(Random.Range(-70, -55), Random.Range(-180, 180), 0));
        for (int i = 0; i < 10; i++)
            Instantiate(Resource2Prefab, Vector3.zero, Quaternion.Euler(Random.Range(-70, -55), Random.Range(-180, 180), 0));
        for (int i = 0; i < 10; i++)
            Instantiate(Resource3Prefab, Vector3.zero, Quaternion.Euler(Random.Range(-70, -55), Random.Range(-180, 180), 0));
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
        float eulerY = Random.value > 0.5f ? yRot - 17 : yRot + 17;
        eulerY += Random.Range(-3f, 3f);
        Instantiate(GoblinPrefab, Vector3.zero, Quaternion.Euler(Random.Range(-70, -55), Random.Range(yRot - 20, yRot + 20), 0));
    }

    /*
    IEnumerator DeductSecond()
    {
        yield return new WaitForSeconds(1);
        seconds -= 1;
        //timeDisplay.GetComponent<Text>().text = seconds.ToString();
        deductingTime = false;

        if (seconds <= 0)
        {
            SceneManager.LoadScene("EscenaFin", LoadSceneMode.Single);
            print("0 segundos");
        }

        print("segundos" + seconds);
    }
    */
}
