using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEnemy : MonoBehaviour
{
    
   

    private void Update()
    {
        
        Destroy(gameObject, 2f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.health -= 20;
            }

            // D�truire la balle apr�s l'avoir utilis�e
            Destroy(gameObject);
        }

    }
    
}
