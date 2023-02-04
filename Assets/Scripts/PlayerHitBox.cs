using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    public int Damage;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RJGoblin>() != null)
        {
            other.GetComponent<RJGoblin>().ApplyDamage(Damage);

            // TODO VFX & audio
        }
    }

}
