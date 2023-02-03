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
        CharacterController.Move(transform.right * Time.deltaTime * 8 * Input.GetAxisRaw("Horizontal"));
        transform.rotation = Quaternion.Euler(0, 0, -(Mathf.Atan2(transform.position.x, transform.position.y) * Mathf.Rad2Deg));
    }
}
