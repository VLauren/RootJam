using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RJChar : MonoBehaviour
{
    public static RJChar Instance { get; private set; }

    public static RJResource currentResource = null;
    public static bool canGather = false;

    CharacterController CharacterController;

    public int hVelocity = 8;
    public int vVelocity = 8;
    public float minZMovement = -5;
    public float maxZMovement = 5;

    public bool canMove = true;

    [Header("Attack")]
    public GameObject Attack1Area;
    public GameObject Attack2Area;
    public float AttackDuration = 0.5f;
    float AttackTimeRemaining = 0f;

    Vector3 AnglePos;
    Quaternion TargetRotation;

    // ========================================

    Animator Lvl1Animator;
    Animator Lvl2Animator;
    Animator Lvl3Animator;

    public int CurrentLevel { get; private set; }



    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        CharacterController = GetComponent<CharacterController>();
        Lvl1Animator = transform.Find("Model/Lvl1").GetComponent<Animator>();
        Lvl2Animator = transform.Find("Model/Lvl2").GetComponent<Animator>();
        Lvl3Animator = transform.Find("Model/Lvl3").GetComponent<Animator>();

        AnglePos = transform.rotation.eulerAngles;

        CharacterController.enabled = false;
        CurrentLevel = 0;

        // sonido inicio juego
        RJAudio.AudioSource.SetIntVar("sfxvar", 11);
        RJAudio.AudioSource.Play("sfx");
    }

    void Update()
    {
        if (canMove)
        {
            Movement();

            if (CurrentLevel < 1)
            {
                transform.Find("Model/Lvl1").gameObject.SetActive(true);
                transform.Find("Model/Rooted1").gameObject.SetActive(false);
                transform.Find("Model/Lvl2").gameObject.SetActive(false);
                transform.Find("Model/Rooted2").gameObject.SetActive(false);
                transform.Find("Model/Lvl3").gameObject.SetActive(false);
                transform.Find("Model/Rooted3").gameObject.SetActive(false);

            }
            else if (CurrentLevel == 1)
            {
                transform.Find("Model/Lvl1").gameObject.SetActive(false);
                transform.Find("Model/Rooted1").gameObject.SetActive(false);
                transform.Find("Model/Lvl2").gameObject.SetActive(true);
                transform.Find("Model/Rooted2").gameObject.SetActive(false);
                transform.Find("Model/Lvl3").gameObject.SetActive(false);
                transform.Find("Model/Rooted3").gameObject.SetActive(false);

            }
            else if (CurrentLevel == 2)
            {
                transform.Find("Model/Lvl1").gameObject.SetActive(false);
                transform.Find("Model/Rooted1").gameObject.SetActive(false);
                transform.Find("Model/Lvl2").gameObject.SetActive(false);
                transform.Find("Model/Rooted2").gameObject.SetActive(false);
                transform.Find("Model/Lvl3").gameObject.SetActive(true);
                transform.Find("Model/Rooted3").gameObject.SetActive(false);
            }

        }

        if (!canMove)
        {
            if (CurrentLevel < 1)
            {
                transform.Find("Model/Lvl1").gameObject.SetActive(false);
                transform.Find("Model/Rooted1").gameObject.SetActive(true);
                transform.Find("Model/Lvl2").gameObject.SetActive(false);
                transform.Find("Model/Rooted2").gameObject.SetActive(false);
                transform.Find("Model/Lvl3").gameObject.SetActive(false);
                transform.Find("Model/Rooted3").gameObject.SetActive(false);
            }
            else if (CurrentLevel == 1)
            {
                transform.Find("Model/Lvl1").gameObject.SetActive(false);
                transform.Find("Model/Rooted1").gameObject.SetActive(false);
                transform.Find("Model/Lvl2").gameObject.SetActive(false);
                transform.Find("Model/Rooted2").gameObject.SetActive(true);
                transform.Find("Model/Lvl3").gameObject.SetActive(false);
                transform.Find("Model/Rooted3").gameObject.SetActive(false);

            }
            else if (CurrentLevel == 2)
            {
                transform.Find("Model/Lvl1").gameObject.SetActive(false);
                transform.Find("Model/Rooted1").gameObject.SetActive(false);
                transform.Find("Model/Lvl2").gameObject.SetActive(false);
                transform.Find("Model/Rooted2").gameObject.SetActive(false);
                transform.Find("Model/Lvl3").gameObject.SetActive(false);
                transform.Find("Model/Rooted3").gameObject.SetActive(true);
            }
        }

        if (Input.GetButtonDown("Fire1") && currentResource != null)
        {
            //una vez se pulsa el boton se para el movimiento y lo pone como lo contrario a lo que esté
            canMove = !canMove;

            print("aqui tiene que estar agachadito");

            RJPlanet.ChangeMaterial(!canMove);
            RJCam.Instance.MovementActive = canMove;

            canGather = !canGather;

            RJVisualFX.Effect(2, transform.position);

            // Audio plantarse
            if(!canMove)
            {
                if(CurrentLevel == 0)
                {
                    RJAudio.AudioSource.SetIntVar("sfxvar", 13);
                    RJAudio.AudioSource.Play("sfx");
                }
                if(CurrentLevel == 1)
                {
                    RJAudio.AudioSource.SetIntVar("sfxvar", 14);
                    RJAudio.AudioSource.Play("sfx");
                }
                if(CurrentLevel == 2)
                {
                    RJAudio.AudioSource.SetIntVar("sfxvar", 15);
                    RJAudio.AudioSource.Play("sfx");
                }
            }
        }

        //machacar el botón para para que el chonko recoja recursos
        if (Input.GetButtonDown("Fire2") && canGather)
        {
            // si da tiempo a implementar diferentes tipos de recursos, es esto + cambiar cosas en el slider smash
            // if (CurrentLevel == 0 /* && RJResource.resourceLevel == 1 */)

            ResourceGather(RJGame.resource1Points, RJGame.resource1Size);
            print("recurso lvl1");
            // }
            // else if (CurrentLevel <= 1 /* && RJResource.resourceLevel == 2 */)
            // {
            //     ResourceGather(RJGame.resource2Points, RJGame.resource2Size);
            //     print("recurso lvl2");
            // }
            // else if (CurrentLevel <= 2 /* && RJResource.resourceLevel == 3 */)
            // {
            //     ResourceGather(RJGame.resource3Points, RJGame.resource3Size);
            //     print("recurso lvl3");
            // }
            // else if (CurrentLevel <= 3 /* && RJResource.resourceLevel == 4 */)
            // {
            //     ResourceGather(RJGame.resource4Points, RJGame.resource4Size);
            //     print("recurso lvl4");
            // }

            print("punticos " + RJGame.growthPoints);
        }

        Evolution();
        AttackLevel1();
        AttackLevel2();





    }

    void Movement()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveInput.Normalize();

        // Movimiento
        RJUtil.SphereMove(transform, new Vector3(-Time.deltaTime * vVelocity * moveInput.z, Time.deltaTime * hVelocity * moveInput.x, 0));

        // Orientacion del modelo
        if (moveInput != Vector3.zero)
        {
            TargetRotation = Quaternion.LookRotation(-moveInput, Vector3.up);
            transform.Find("Model").localRotation = Quaternion.RotateTowards(transform.Find("Model").localRotation, TargetRotation, Time.deltaTime * 360);
        }

        Lvl1Animator.SetFloat("Speed", moveInput.magnitude);
        Lvl2Animator.SetFloat("Speed", moveInput.magnitude);
        Lvl3Animator.SetFloat("Speed", moveInput.magnitude);
    }

    void ResourceGather(int resourcePoints, int resourceSize)
    {
        //cogemos los puntos de recursos, sumamos a los puntos del jugador por cada vez que pulse la tecla, y eso se comprueba con checkcurrent para cuando suba de nivel

        RJGame.currentGatherPoints += resourcePoints;
        print("currentGatherPoints " + RJGame.currentGatherPoints);

        RJAudio.AudioSource.SetIntVar("sfxvar", 8);
        RJAudio.AudioSource.Play("sfx");

        if (RJGame.currentGatherPoints == resourceSize)
        {
            RJGame.growthPoints += resourceSize;

            //resetea todo
            RJGame.currentGatherPoints = 0;
            canGather = !canGather;
            canMove = !canMove;
            RJPlanet.ChangeMaterial(!canMove);
            RJCam.Instance.MovementActive = canMove;
            Destroy(RJChar.currentResource.gameObject);

            RJAudio.AudioSource.SetIntVar("sfxvar", 10);
            RJAudio.AudioSource.Play("sfx");
        }

        //RJGame.playerResources += RJGame.growthPoints;

        RJGame.CheckCurrentLevel();
    }

    void Evolution()
    {
        if (CurrentLevel < 1 && RJGame.CheckCurrentLevel() == 1)
        {
            // fase 2
            CurrentLevel = 1;

            transform.Find("Model/Lvl1").gameObject.SetActive(false);
            transform.Find("Model/Rooted1").gameObject.SetActive(false);
            transform.Find("Model/Lvl2").gameObject.SetActive(true);

            AttackTimeRemaining = 0.5f;
        }
        if (CurrentLevel < 2 && RJGame.CheckCurrentLevel() == 2)
        {
            // fase 3
            CurrentLevel = 2;

            transform.Find("Model/Lvl2").gameObject.SetActive(false);
            transform.Find("Model/Rooted2").gameObject.SetActive(false);
            transform.Find("Model/Lvl3").gameObject.SetActive(true);

            AttackTimeRemaining = 0.5f;
        }
        if (CurrentLevel < 3 && RJGame.CheckCurrentLevel() == 3)
        {
            // splosion
            RJPlanetSpawner spawner = FindObjectOfType<RJPlanetSpawner>();
            spawner.StartCoroutine(spawner.NewPlanetRoutine());
        }

        // HACK btnes de debug

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            RJGame.growthPoints = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            RJGame.growthPoints = 20;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            RJGame.growthPoints = 40;
        }
    }



    void AttackLevel1()
    {
        if (!canMove || CurrentLevel != 1) return;

        if (AttackTimeRemaining > 0)
            AttackTimeRemaining -= Time.deltaTime;
        else
        {
            AttackTimeRemaining = 0f;
            Attack1Area.gameObject.SetActive(false);
        }

        if (AttackTimeRemaining > 0)
            return;

        if (Input.GetButtonDown("Fire2"))
            StartCoroutine(Level1Attack());
    }

    IEnumerator Level1Attack()
    {
        AttackTimeRemaining = AttackDuration;

        Lvl2Animator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.1f);

        RJAudio.AudioSource.SetIntVar("sfxvar", 6);
        RJAudio.AudioSource.Play("sfx");

        // Vector3 FXPos = transform.Find("AtkFXPos").position;
        // var fx = WJVisualFX.Effect(2, FXPos, Quaternion.Euler(0, -90, 0) * transform.rotation);
        // fx.transform.parent = transform;

        // TODO camera shake

        yield return new WaitForSeconds(0.1f);

        Attack1Area.gameObject.SetActive(true);
    }

    void AttackLevel2()
    {
        if (!canMove || CurrentLevel != 2) return;

        if (AttackTimeRemaining > 0)
            AttackTimeRemaining -= Time.deltaTime;
        else
        {
            AttackTimeRemaining = 0f;
            Attack2Area.gameObject.SetActive(false);
        }

        if (AttackTimeRemaining > 0)
            return;

        if (Input.GetButtonDown("Fire2"))
            StartCoroutine(Level2Attack());
    }

    IEnumerator Level2Attack()
    {
        AttackTimeRemaining = AttackDuration;

        Lvl3Animator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.1f);

        RJAudio.AudioSource.SetIntVar("sfxvar", 7);
        RJAudio.AudioSource.Play("sfx");

        // Vector3 FXPos = transform.Find("AtkFXPos").position;
        // var fx = WJVisualFX.Effect(2, FXPos, Quaternion.Euler(0, -90, 0) * transform.rotation);
        // fx.transform.parent = transform;

        // TODO camera shake

        yield return new WaitForSeconds(0.1f);

        Attack2Area.gameObject.SetActive(true);
    }

    // IEnumerator DeductSecond()
    // {
    //     yield return new WaitForSeconds(1);
    //     seconds -= 1;
    //     //timeDisplay.GetComponent<Text>().text = seconds.ToString();
    //     deductingTime = false;

    //     if (seconds <= 0)
    //     {
    //         SceneManager.LoadScene("EscenaFin", LoadSceneMode.Single);
    //         print("0 segundos");
    //     }

    //     print("segundos" + seconds);
    // }
}
