using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobAttack : MonoBehaviour
{
    public BlobController playerControlEnemy;
    public GameObject enemy;
    public GameObject bea;
    public Rigidbody rigidbodyBea;
    public Player_Control playerControlBea;

    public bool touchOk;


    private void Start()
    {
        rigidbodyBea = bea.GetComponent<Rigidbody>();
        playerControlBea = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Control>();
        touchOk = false;
    }
    private void Update()
    {
        float delta = Time.deltaTime;
        playerControlEnemy.HandleAttack(delta);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && touchOk && playerControlEnemy.isAttacking)
        {
            playerControlBea.health -= 20;
            touchOk = false;
            
        }
    }

}

