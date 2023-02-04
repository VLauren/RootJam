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
        if(moveInput != Vector3.zero)
        {
            TargetRotation = Quaternion.LookRotation(moveInput, Vector3.up);
            transform.Find("Model").localRotation = Quaternion.RotateTowards(transform.Find("Model").localRotation, TargetRotation, Time.deltaTime * 800);
        }
    }
}
