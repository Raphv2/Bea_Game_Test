using UnityEngine;

public class RepulsionScript : MonoBehaviour
{
    public float repulsionForce;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {

            GameObject enemy = collision.gameObject;
            Rigidbody enemyRigidbody = enemy.GetComponent<Rigidbody>();

            if (enemyRigidbody != null)
            {
                
                Vector3 contactPoint = collision.contacts[0].point; 
                Vector3 repulsionDirection = transform.position - contactPoint; 

               
                enemyRigidbody.AddForce(-repulsionDirection.normalized * repulsionForce, ForceMode.Impulse);
            }
        }
    }
}