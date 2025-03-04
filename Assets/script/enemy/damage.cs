using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damage : MonoBehaviour
{

    public PlayerControl playerControlEnemy;
    public GameObject enemy;
    public GameObject bea;
    public Rigidbody rigidbodyBea;
    


    public bool touch;
    public float touchOne = 0;
    [SerializeField]
    private LayerMask layerSmash;

    public float heightSword = 10f;

    private void Start()
    {
        rigidbodyBea = bea.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        float delta = Time.deltaTime;
        HandlingAttack(delta);
    }

    private void HandlingAttack(float delta)
    {
        Vector3 direction = bea.transform.position - enemy.transform.position;
        
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, heightSword, layerSmash) && playerControlEnemy.isAttacking) touch = true;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * heightSword, Color.red);

        if (touch)
        {
            rigidbodyBea.AddForce(direction.normalized * 0.75f, ForceMode.Impulse);
            
        }
        

    }

    
}

