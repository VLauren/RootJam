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

    public static float RemainingTime { get; private set; }

    public static int gameScore;


    private void Awake()
    {
        RemainingTime = 60;
    }

    void Start()
    {
        SpawnPlanet(0);
        Camera.main.backgroundColor = CurrentPlanet.GetComponent<RJPlanet>().BackgroundColor;

        RJAudio.AudioSource.SetIntVar("musicvar", 1);
        // RJAudio.AudioSource.SetIntVar("littlevolume", 1);
        RJAudio.AudioSource.Play("musica");

        // sonido inicio juego
        RJAudio.AudioSource.SetIntVar("sfxvar", 11);
        RJAudio.AudioSource.Play("sfx");

        // RJAudio.AudioSource.SetIntVar("crushvar", 1);
        gameScore += RJGame.growthPoints;

        RJVisualFX.Effect(2, CurrentPlayer.transform.position);
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
        if (RemainingTime <= 0 && !gameover)
        {
            gameover = true;
            StartCoroutine(GameOver());
        }
        // else
        // print(RemainingTime);


        gameScore = RJGame.growthPoints;
    }

    bool gameover = false;
    public IEnumerator GameOver()
    {
        // TODO Anim muerte jug
        CurrentPlayer.transform.GetChild(0).GetComponent<RJChar>().enabled = false;
        var camScr = Camera.main.GetComponent<RJCam>();
        camScr.enabled = false;

        // Audio pierde
        RJAudio.AudioSource.SetIntVar("sfxvar", 12);
        RJAudio.AudioSource.Play("sfx");

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("EscenaFin", LoadSceneMode.Single);
    }

    public IEnumerator NewPlanetRoutine()
    {
        RemainingTime += 30;

        CurrentPlayer.transform.GetChild(0).GetComponent<RJChar>().enabled = false;
        var camScr = Camera.main.GetComponent<RJCam>();
        camScr.enabled = false;

        // Audio planeta explota
        RJAudio.AudioSource.SetIntVar("sfxvar", 16);
        RJAudio.AudioSource.Play("sfx");


        while (camScr.transform.localEulerAngles.x > -30 + 360)
        {
            camScr.transform.localEulerAngles = new Vector3(camScr.transform.localEulerAngles.x - Time.deltaTime * 5, camScr.transform.localEulerAngles.y, 0);
            camScr.transform.localPosition = new Vector3(0, 0, camScr.transform.localPosition.z + Time.deltaTime * 50);
            yield return null;
        }

        yield return null;

        RJAudio.AudioSource.SetIntVar("sfxvar", 17);
        RJAudio.AudioSource.Play("sfx");

        SpawnPlanet(Random.Range(1, 4));
        CurrentPlayer.transform.eulerAngles = new Vector3(-59, camScr.transform.parent.eulerAngles.y - 180, 0);

        yield return null;

        CurrentPlayer.SetActive(false);
        // camScr.transform.localPosition = new Vector3(0, 0, 200);

        yield return null;

        while (camScr.transform.localEulerAngles.x < -5 + 360)
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

        // sonido inicio juego
        RJAudio.AudioSource.SetIntVar("sfxvar", 11);
        RJAudio.AudioSource.Play("sfx");

        RJVisualFX.Effect(2, CurrentPlayer.transform.position);
    }

}
