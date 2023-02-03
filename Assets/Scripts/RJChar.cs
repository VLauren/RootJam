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

    float distToCenter;

    Vector3 AnglePos;

    void Start()
    {
        CharacterController = GetComponent<CharacterController>();

        distToCenter = transform.position.magnitude;

        AnglePos = transform.rotation.eulerAngles;

        CharacterController.enabled = false;
    }

    void Update()
    {
        Movement();

        // - puslo un boton (GetButton Fire?)
        // - cuando lo pulso, no me puedo mover
        // - cuando lo puslo otra vez, me puedo volver a mover
    }

    public Transform Dummy;

    void Movement()
    {
        // Magia, no pregunteis
        Dummy.transform.parent.rotation = Quaternion.Euler(
            Mathf.Clamp(Dummy.transform.parent.rotation.eulerAngles.x - Time.deltaTime * vVelocity * Input.GetAxisRaw("Vertical"), 290, 304),
            Dummy.transform.parent.rotation.eulerAngles.y - Time.deltaTime * hVelocity * Input.GetAxisRaw("Horizontal"),
            Dummy.transform.parent.rotation.eulerAngles.z);

        Debug.Log(Dummy.transform.parent.rotation.eulerAngles);

        transform.position = Dummy.transform.position;
        transform.rotation = Dummy.transform.rotation;
    }
}
