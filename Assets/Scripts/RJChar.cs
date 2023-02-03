using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RJChar : MonoBehaviour
{
    CharacterController CharacterController;

    public int velocity = 8;
    public float minZMovement = -5;
    public float maxZMovement = 5;

    public bool canMove = true;

    void Start()
    {
        CharacterController = GetComponent<CharacterController>();
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
        }
    }

    void Movement()
    {
        // Muevo el personaje
        Vector3 horizontalMovement = transform.right * Time.deltaTime * velocity * Input.GetAxisRaw("Horizontal");
        Vector3 verticalMovement = transform.forward * Time.deltaTime * velocity * Input.GetAxisRaw("Vertical");
        CharacterController.Move(horizontalMovement + verticalMovement);

        // Cambio la rotaci�n del personaje
        transform.rotation = Quaternion.Euler(Mathf.Atan2(transform.position.z, transform.position.y) * Mathf.Rad2Deg, 0, -(Mathf.Atan2(transform.position.x, transform.position.y) * Mathf.Rad2Deg));

        if (transform.position.z <= minZMovement)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, minZMovement);
        }

        if (transform.position.z >= maxZMovement)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, maxZMovement);
        }

    }
}
