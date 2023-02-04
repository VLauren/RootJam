using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damage = 10;
    public float Speed;

    float Elapsed = 0;

    void Start()
    {
        if (RJChar.Instance != null)
            transform.LookAt(RJChar.Instance.transform.position);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);

        Elapsed += Time.deltaTime;
        if (Elapsed >= 5)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RJChar>() != null)
        {
            // Al pegar quito recursos
            RJGame.AddGrowthPoints(-damage);

            Destroy(gameObject);
        }
    }
}
