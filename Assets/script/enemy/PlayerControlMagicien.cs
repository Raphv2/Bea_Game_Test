using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerControlMagicien : MonoBehaviour
{
    public Player_Control playerControlBea;
    public EnemyHealth enemyHealth;

    
    public bool walk;
    public bool follow;
    public bool isGrounded;
    public bool followPlayer = false;
    public bool InvocationOk = true;

    public Transform beaTransform;
    public Transform enemyTransform;
   
    public NavMeshAgent agent;

    float distanceBea;
    public float distanceminimale = 1000f;
    public float distanceAttack = 900f;
    float timerMax = 5f;
    public bool isAttacking;
    public float timerAttack;

    public Rigidbody rb;
    public Animator Anim;
    // Start is called before the first frame update
    public LayerMask playerLayerMask;

    private GameObject player;
    private int numberAttack;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        beaTransform = player.transform;

        rb = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();

        enemyHealth = this.GetComponent<EnemyHealth>();

    }

    void Update()
    {
        Vector3 direction = beaTransform.position - transform.position;
        
        //Quaternion rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), f);
       // rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, 0);
       // transform.rotation = rotation;
     
        distanceBea = Vector3.Distance(enemyTransform.position, beaTransform.position);
        float delta = Time.deltaTime;
        

        if (!enemyHealth.death)
        {
            
            

            if(numberAttack == 0) HandleAttack(delta, direction);

            if (numberAttack == 1 && InvocationOk)
            {
                HandleInvocation(delta, direction);
                InvocationOk = false;
            }
            else HandleAttack(delta, direction);



            if (!isAttacking)
            {
                FollowBea();

                if (distanceBea < distanceAttack)
                {

                    numberAttack = Random.Range(0, 2);

                }
                
            }
            
        }
        

    }

    

    private void FollowBea()
    {
        
        Anim.SetBool("walk", walk);

        //print(hitwall(distanceBea));
        if (distanceminimale > distanceBea &&  distanceBea > distanceAttack )
        {
            // print(hitwall(distanceBea));
            if (hitwall(distanceBea) == true)
            {
                
                walk = true;

                if(followPlayer) agent.SetDestination(beaTransform.position);



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

    private void HandleAttack(float delta, Vector3 direction)
    {


        if (distanceBea < distanceAttack && timerAttack < 0.2f)
        {
            
            isAttacking = true;
            Quaternion rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), 0.1f);
             rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, 0);
            transform.rotation = rotation;

            Anim.SetTrigger("cast");
            timerAttack += delta;


        }

        

        if (timerAttack != 0f && timerAttack <= timerMax)
        {
            timerAttack += delta;
            walk = false;
        }

        if (timerAttack >= timerMax)
        {
            isAttacking = false;
            timerAttack = 0;
        }



    }

    private void HandleInvocation(float delta, Vector3 direction)
    {


        if (distanceBea < distanceAttack && timerAttack < 0.2f)
        {

            isAttacking = true;
            Quaternion rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), 0.1f);
            rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, 0);
            transform.rotation = rotation;

            Anim.SetTrigger("invocation");
            timerAttack += delta;


        }



        if (timerAttack != 0f && timerAttack <= timerMax)
        {
            timerAttack += delta;
            walk = false;
        }

        if (timerAttack >= timerMax)
        {
            isAttacking = false;
            timerAttack = 0;
        }



    }

    private void ChangeDefautAnimation()
    {
        Anim.Play("idle", 0, 0);
        followPlayer = true;
    }
}