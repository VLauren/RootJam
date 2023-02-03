using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RJChar : MonoBehaviour
{
    CharacterController CharacterController;

    int velocity = 8;

    float minZMovement = -5;

    float maxZMovement = 5;

    void Start()
    {
        CharacterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Muevo el personaje
        Vector3 horizontalMovement = transform.right * Time.deltaTime * velocity * Input.GetAxisRaw("Horizontal");
        Vector3 verticalMovement = transform.forward * Time.deltaTime * velocity * Input.GetAxisRaw("Vertical");
        CharacterController.Move(horizontalMovement + verticalMovement);

        // Cambio la rotaci�n del personaje
        transform.rotation = Quaternion.Euler(Mathf.Atan2(transform.position.z, transform.position.y) * Mathf.Rad2Deg, 0, -(Mathf.Atan2(transform.position.x, transform.position.y) * Mathf.Rad2Deg));

        if (transform.position.z <= minZMovement)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -5);
        }

        if (transform.position.z >= maxZMovement)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 5);
        }
    }
}
