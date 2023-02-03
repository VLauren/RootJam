using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RJChar : MonoBehaviour
{
    CharacterController CharacterController;

    void Start()
    {
        CharacterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Muevo el personaje
        Vector3 horizontalMovement = transform.right * Time.deltaTime * 8 * Input.GetAxisRaw("Horizontal");
        Vector3 verticalMovement = transform.forward * Time.deltaTime * 8 * Input.GetAxisRaw("Vertical");
        CharacterController.Move(horizontalMovement + verticalMovement);

        // Cambio la rotación del personaje
        transform.rotation = Quaternion.Euler(Mathf.Atan2(transform.position.z, transform.position.y) * Mathf.Rad2Deg, 0, -(Mathf.Atan2(transform.position.x, transform.position.y) * Mathf.Rad2Deg));
    }
}
