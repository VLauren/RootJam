using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RJPlanetSpawner : MonoBehaviour
{
    public List<GameObject> Planets;
    public GameObject PlayerPrefab;

    GameObject CurrentPlanet;
    GameObject CurrentPlayer;

    void Start()
    {
        SpawnPlanet();
    }

    void SpawnPlanet()
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
        CurrentPlanet = Instantiate(Planets[0], Vector3.zero, Quaternion.identity);
        CurrentPlayer = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.Euler(-59, 0, 0));
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.N))
            // StartCoroutine(NewPlanetRoutine());
    }

    public IEnumerator NewPlanetRoutine()
    {
        CurrentPlayer.transform.GetChild(0).GetComponent<RJChar>().enabled = false;
        var camScr = Camera.main.GetComponent<RJCam>();
        camScr.enabled = false;

        // Camera.main.backgroundColor
        // -50

        while(camScr.transform.localEulerAngles.x > -30 + 360)
        {
            camScr.transform.localEulerAngles = new Vector3(camScr.transform.localEulerAngles.x - Time.deltaTime * 5, camScr.transform.localEulerAngles.y, 0);
            camScr.transform.localPosition = new Vector3(0, 0, camScr.transform.localPosition.z + Time.deltaTime * 50);
            yield return null;
        }

        yield return null;

        SpawnPlanet();
        CurrentPlayer.transform.eulerAngles = new Vector3(-59, camScr.transform.parent.eulerAngles.y - 180, 0);

        yield return null;

        CurrentPlayer.SetActive(false);
        // camScr.transform.localPosition = new Vector3(0, 0, 200);

        yield return null;

        while(camScr.transform.localEulerAngles.x < -5 + 360)
        {
            camScr.transform.localEulerAngles = new Vector3(camScr.transform.localEulerAngles.x + Time.deltaTime * 30, camScr.transform.localEulerAngles.y, 0);
            yield return null;
        }

        yield return new WaitForSeconds(1);

        camScr.enabled = true;

        yield return new WaitForSeconds(1);
        CurrentPlayer.SetActive(true);
    }

}
