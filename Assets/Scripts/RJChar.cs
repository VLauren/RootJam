using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RJChar : MonoBehaviour
{
    CharacterController CharacterController;

    public int hVelocity = 8;
    public int vVelocity = 8;
    public float minZMovement = -5;
    public float maxZMovement = 5;

    public bool canMove = true;
    public bool canGather = false;

    float distToCenter;

    Vector3 AnglePos;

    public Transform Dummy;


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
        // Magia, no pregunteis
        Dummy.transform.parent.rotation = Quaternion.Euler(
            Mathf.Clamp(Dummy.transform.parent.rotation.eulerAngles.x - Time.deltaTime * vVelocity * Input.GetAxisRaw("Vertical"), 290, 304),
            Dummy.transform.parent.rotation.eulerAngles.y - Time.deltaTime * hVelocity * Input.GetAxisRaw("Horizontal"),
            Dummy.transform.parent.rotation.eulerAngles.z);

        transform.position = Dummy.transform.position;
        transform.rotation = Dummy.transform.rotation;
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
