using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RJChar : MonoBehaviour
{
    public static RJChar Instance { get; private set; }

    CharacterController CharacterController;

    public int hVelocity = 8;
    public int vVelocity = 8;
    public float minZMovement = -5;
    public float maxZMovement = 5;

    public bool canMove = true;
    public bool canGather = false;

    float distToCenter;

    Vector3 AnglePos;
    Quaternion TargetRotation;


    public Transform Dummy;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        CharacterController = GetComponent<CharacterController>();

        distToCenter = transform.position.magnitude;

        AnglePos = transform.rotation.eulerAngles;

        CharacterController.enabled = false;
    }

    void Update()
    {
        if (canMove)
        {
            Movement();
        }

        if (Input.GetButtonDown("Fire1"))
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

    }

    void Movement()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        // Movimiento
        RJUtil.SphereMove(transform, new Vector3(
            Time.deltaTime * vVelocity * moveInput.z,
            Time.deltaTime * hVelocity * moveInput.x,
            0));

        // Orientacion del modelo
        if (moveInput != Vector3.zero)
        {
            TargetRotation = Quaternion.LookRotation(moveInput, Vector3.up);
            transform.Find("Model").localRotation = Quaternion.RotateTowards(transform.Find("Model").localRotation, TargetRotation, Time.deltaTime * 800);
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

            print("PUNTASO Y FUERA");
        }

        //RJGame.playerResources += RJGame.growthPoints;

        RJGame.CheckCurrentLevel();
    }
}
