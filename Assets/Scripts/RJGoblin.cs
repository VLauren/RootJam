using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RJGoblin : MonoBehaviour
{
    public int HVelocity = 8;
    public int VVelocity = 8;

    public float TargetDistance;

    [Space()]
    public float FireRate;
    public GameObject Projectile;

    protected Animator Anim;

    Quaternion TargetRotation;

    public int healthPoints = 30;

    void Start()
    {
        StartCoroutine(Shooting()); // Anyways I started blasting

        Anim = transform.Find("Model/Goblin").GetComponent<Animator>();
    }

    void Update()
    {
        if (RJChar.Instance == null)
            return;

        Vector3 dir = transform.InverseTransformPoint(RJChar.Instance.transform.position);
        dir.y = 0;
        dir.Normalize();

        // Movimiento
        bool movZero = false;
        if (Vector3.Distance(transform.position, RJChar.Instance.transform.position) > TargetDistance)
            RJUtil.SphereMove(transform, new Vector3(Time.deltaTime * VVelocity * dir.z, -Time.deltaTime * HVelocity * dir.x, 0));
        else
            movZero = true; // movimiento zero

        // Rotation
        // if (!movZero)
        {
            TargetRotation = Quaternion.LookRotation(-dir, Vector3.up);
            transform.Find("Model").localRotation = Quaternion.RotateTowards(transform.Find("Model").localRotation, TargetRotation, Time.deltaTime * 360);
        }

        if (Anim != null)
        {
            Anim.SetFloat("Speed", dir.magnitude);
        }
    }

    IEnumerator Shooting()
    {
        while (true)
        {
            yield return new WaitForSeconds((0.9f + Random.value * 0.2f) / FireRate);
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        Anim.SetTrigger("Attack");
        // print("pium");

        yield return new WaitForSeconds(0.3f);

        if (Projectile != null)
            Instantiate(Projectile, transform.Find("ProjectileSpawnPoint").position, Quaternion.identity);

        RJAudio.AudioSource.SetIntVar("sfxvar", 4);
        RJAudio.AudioSource.Play("sfx");
    }

    // TODO da�o y muerte

    public void ApplyDamage(int damage)
    {
        healthPoints -= damage;
        if (healthPoints <= 0)
        {

            Destroy(gameObject);
        }
    }
}
