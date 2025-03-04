using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHammer : MonoBehaviour
{
    public Animation_Player animation_Player;
    public GameObject Bea;

    public float Damage = 20;
    public bool damage;
    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            Rigidbody enemyRigidbody = other.GetComponent<Rigidbody>();
            Vector3 direction = other.transform.position - Bea.transform.position;
            if (!animation_Player.isAttacking)
            {
                if (enemyHealth != null && damage)
                {
                    enemyHealth.health -= Damage;
                    enemyRigidbody.AddForce(direction * 10, ForceMode.Impulse);
                    damage = false;
                }

            }


        }

    }

}
