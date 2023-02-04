using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    public float AttackDuration = 0.5f;
    float AttackTimeRemaining = 0f;

    Vector3 AnglePos;
    Quaternion TargetRotation;

    public int CurrentLevel { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        CharacterController = GetComponent<CharacterController>();

        AnglePos = transform.rotation.eulerAngles;

        CharacterController.enabled = false;
        CurrentLevel = 0;
    }

    void Update()
    {
        if (canMove)
        {
            Movement();
        }

        if (Input.GetButtonDown("Fire1") && currentResource != null)
        {
            //una vez se pulsa el boton se para el movimiento y lo pone como lo contrario a lo que esté
            canMove = !canMove;

            RJPlanet.ChangeMaterial(!canMove);
            RJCam.Instance.MovementActive = canMove;

            canGather = !canGather;

        }

        //machacar el botón para para que el chonko recoja recursos
        if (Input.GetButtonDown("Fire2") && canGather)
        {
            ResourceGather();
            print("punticos " + RJGame.growthPoints);
        }

        Evolution();
        AttackLevel1();
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
    }

    void ResourceGather()
    {
        //cogemos los puntos de recursos, sumamos a los puntos del jugador por cada vez que pulse la tecla, y eso se comprueba con checkcurrent para cuando suba de nivel


        RJGame.currentGatherPoints += RJGame.resource1Points;
        print("currentGatherPoints " + RJGame.currentGatherPoints);
        if (RJGame.currentGatherPoints == RJGame.resource1Size)
        {
            RJGame.growthPoints += RJGame.resource1Size;

            //resetea todo
            RJGame.currentGatherPoints = 0;
            canGather = !canGather;
            canMove = !canMove;
            RJPlanet.ChangeMaterial(!canMove);
            RJCam.Instance.MovementActive = canMove;
            Destroy(RJChar.currentResource.gameObject);

            print("PUNTASO Y FUERA");
        }

        //RJGame.playerResources += RJGame.growthPoints;

        RJGame.CheckCurrentLevel();
    }

    void Evolution()
    {
        if(CurrentLevel < 1 && RJGame.CheckCurrentLevel() == 1)
        {
            // fase 2
            print("EVOLUSIONA");

            CurrentLevel = 1;

            transform.Find("Model/Lvl1").gameObject.SetActive(false);
            transform.Find("Model/Rooted1").gameObject.SetActive(false);
            transform.Find("Model/Lvl2").gameObject.SetActive(true);

            AttackTimeRemaining = 0.5f;
        }
        if(CurrentLevel < 2 && RJGame.CheckCurrentLevel() == 2)
        {
            // fase 3
        }
        if(CurrentLevel < 3 && RJGame.CheckCurrentLevel() == 3)
        {
            // splosion
        }

        // HACK btnes de debug

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            RJGame.growthPoints = 1; 
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            RJGame.growthPoints = 10; 
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            RJGame.growthPoints = 20; 
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

        // TODO animation attack trigger

        yield return new WaitForSeconds(0.1f);

        // Vector3 FXPos = transform.Find("AtkFXPos").position;
        // var fx = WJVisualFX.Effect(2, FXPos, Quaternion.Euler(0, -90, 0) * transform.rotation);
        // fx.transform.parent = transform;

        // TODO camera shake

        yield return new WaitForSeconds(0.1f);

        Attack1Area.gameObject.SetActive(true);
    }

}
