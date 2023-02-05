using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RJPlanetSpawner : MonoBehaviour
{
    public List<GameObject> Planets;
    public GameObject PlayerPrefab;

    GameObject CurrentPlanet;
    GameObject CurrentPlayer;

    public float RemainingTime { get; private set; }

    private void Awake()
    {
        RemainingTime = 180;
    }

    void Start()
    {
        SpawnPlanet(0);
        Camera.main.backgroundColor = CurrentPlanet.GetComponent<RJPlanet>().BackgroundColor;

        // RJAudio.AudioSource.SetIntVar("musicvar", 0);
        // RJAudio.AudioSource.SetIntVar("littlevolume", 1);
        // RJAudio.AudioSource.SetFloatVar("littlevolume", 1);
        // RJAudio.AudioSource.Play("musica");
    }

    void SpawnPlanet(int index)
    {
        if (CurrentPlayer != null)
            Destroy(CurrentPlayer);
        if (CurrentPlanet != null)
            Destroy(CurrentPlanet);

        var goblins = FindObjectsOfType<RJGoblin>();
        foreach (var gob in goblins)
            Destroy(gob.gameObject);

        var resources = FindObjectsOfType<RJResource>();
        foreach (var reso in resources)
            Destroy(reso.gameObject);

        var projectiles = FindObjectsOfType<EnemyProjectile>();
        foreach (var proj in projectiles)
            Destroy(proj.gameObject);

        RJGame.ResetValues();
        CurrentPlanet = Instantiate(Planets[index], Vector3.zero, Quaternion.identity);
        CurrentPlayer = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.Euler(-59, 0, 0));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
            StartCoroutine(NewPlanetRoutine());

        RemainingTime -= Time.deltaTime;
        if (RemainingTime <= 0)
        {
            SceneManager.LoadScene("EscenaFin", LoadSceneMode.Single);
            print("0 segundos");
        }
        else
            print(RemainingTime);
    }

    public IEnumerator NewPlanetRoutine()
    {
        RemainingTime += 30;

        CurrentPlayer.transform.GetChild(0).GetComponent<RJChar>().enabled = false;
        var camScr = Camera.main.GetComponent<RJCam>();
        camScr.enabled = false;

        while(camScr.transform.localEulerAngles.x > -30 + 360)
        {
            camScr.transform.localEulerAngles = new Vector3(camScr.transform.localEulerAngles.x - Time.deltaTime * 5, camScr.transform.localEulerAngles.y, 0);
            camScr.transform.localPosition = new Vector3(0, 0, camScr.transform.localPosition.z + Time.deltaTime * 50);
            yield return null;
        }

        yield return null;

        SpawnPlanet(Random.Range(1,4));
        CurrentPlayer.transform.eulerAngles = new Vector3(-59, camScr.transform.parent.eulerAngles.y - 180, 0);

        yield return null;

        CurrentPlayer.SetActive(false);
        // camScr.transform.localPosition = new Vector3(0, 0, 200);

        yield return null;

        while(camScr.transform.localEulerAngles.x < -5 + 360)
        {
            camScr.transform.localEulerAngles = new Vector3(camScr.transform.localEulerAngles.x + Time.deltaTime * 30, camScr.transform.localEulerAngles.y, 0);

            Camera.main.backgroundColor = new Color
                (Mathf.MoveTowards(Camera.main.backgroundColor.r, CurrentPlanet.GetComponent<RJPlanet>().BackgroundColor.r, Time.deltaTime),
                Mathf.MoveTowards(Camera.main.backgroundColor.g, CurrentPlanet.GetComponent<RJPlanet>().BackgroundColor.g, Time.deltaTime),
                Mathf.MoveTowards(Camera.main.backgroundColor.b, CurrentPlanet.GetComponent<RJPlanet>().BackgroundColor.b, Time.deltaTime)
                );
                
                // Vector3.MoveTowards(Camera.main.backgroundColor, CurrentPlanet.GetComponent<RJPlanet>().BackgroundColor, Time.deltaTime * 10);

            yield return null;
        }

        yield return new WaitForSeconds(1);

        camScr.enabled = true;

        yield return new WaitForSeconds(1);
        CurrentPlayer.SetActive(true);
    }

}
