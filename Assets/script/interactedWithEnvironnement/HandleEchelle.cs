using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleEchelle : MonoBehaviour
{
    public Vector3 rootMotion;

    public Animation_Player animationPlayer;
    public SwitchObjects switchObjects;
   
    public Transform beaTransform;
    public Transform target;
    CharacterController characterController;
    public Animator Anim;

    public bool isInteracted;

   
    public float distanceTarget;
    public float distanceMinimale;

    void Start()
    {
        Anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        beaTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animationPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Animation_Player>();
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        float delta = Time.deltaTime;
        Vector3 velocity;
        distanceTarget = Vector3.Distance (beaTransform.position, target.position);
        if (distanceTarget < distanceMinimale && Input.GetKeyDown(KeyCode.E))
        {
            isInteracted = !isInteracted;
        }

        if (isInteracted)
        {
            beaTransform.rotation = Quaternion.LookRotation(-transform.position);
            beaTransform.position = target.position;
            switchObjects.selectedNumber = 1;
            animationPlayer.climbEchelle = true;
            animationPlayer.Anim.SetLayerWeight(9, 1f);
            animationPlayer.Anim.SetLayerWeight(1, 0f);


            if (vertical > 0)
            {
                Anim.SetBool("ladderwalk", true);
                velocity = new Vector3(0, 5f * vertical * delta,0);
                characterController.Move(  velocity);
                rootMotion = Vector3.zero;
                
            }
            else
            {
                Anim.SetBool("ladderwalk", false);
                
            }
                
        }
        else
        {
            
            Anim.SetBool("ladderwalk", false);
            animationPlayer.climbEchelle = false;
            animationPlayer.Anim.SetLayerWeight(9, 0f);
            
        }
            
            
    }

    private void OnAnimatorMove()
    {
        
            rootMotion += Anim.deltaPosition;
            
        


    }
    // programme qui sert à l'interaction du joueur à lobjet



}
