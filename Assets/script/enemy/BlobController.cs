using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BlobController : MonoBehaviour
{
    public BlobAttack blobAttack;
    public Player_Control playerControlBea;



    
    public bool walk;
    public bool follow;
    public bool isGrounded;

    public Transform beaTransform;
    public Transform enemyTransform;
    //public Transform isGroundedTranform;

    public NavMeshAgent agent;

    float distanceBea;
    public float distanceminimale = 50f;
    public float distanceAttack = 4.1f;
    float timerMax = 2.1f;
    public bool isAttacking;
    public float timerAttack;

    
    public Animator Anim;
    // Start is called before the first frame update
    public LayerMask playerLayerMask;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");


        
        Anim = GetComponent<Animator>();

    }

    void Update()
    {
       
        distanceBea = Vector3.Distance(enemyTransform.position, beaTransform.position);
        float delta = Time.deltaTime;
        //HandleAttack(delta);

        FollowBea();



    }



    private void FollowBea()
    {
        Anim.SetBool("walk", walk);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, 0.5f))
        {
            isGrounded = true;
        }
        else isGrounded = false;

        //print(hitwall(distanceBea));
        if (distanceminimale > distanceBea && isGrounded && distanceBea > distanceAttack && isGrounded)
        {
            // print(hitwall(distanceBea));
            if (hitwall(distanceBea) == true)
            {

                walk = true;

                enemyTransform.LookAt(beaTransform);
                agent.SetDestination(beaTransform.position);

            }


        }

        else
        {
            walk = false;
            agent.SetDestination(transform.position);
        }
    }

    private bool hitwall(float distance)
    {

        RaycastHit hitwall;
        Vector3 direction = beaTransform.position - enemyTransform.position;

        if (Physics.Raycast(transform.position, direction, out hitwall, distance, playerLayerMask) && distanceminimale > distanceBea)
        {
            follow = true;
            //Debug.DrawRay(enemyTransform.position, direction * distance, Color.red);
        }

        else
        {

            follow = false;
        }
        return follow;
    }

    public void HandleAttack(float delta)
    {     
        timerAttack += delta;

        if (distanceBea < distanceAttack)
        {
            if (timerAttack > 2f)
            {
                Vector3 direction = beaTransform.position - transform.position;

                Quaternion rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), 0.1f);
                rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, 0);
                transform.rotation = rotation;

                Anim.SetTrigger("attack");
                isAttacking = true;
            }
        }
        else
        {            
            isAttacking = false;
        }
    }

    private void Attack()
    {
        blobAttack.touchOk = true;
        timerAttack = 0f;
    }
}
