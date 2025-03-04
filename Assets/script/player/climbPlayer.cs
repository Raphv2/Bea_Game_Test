using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class climbPlayer : MonoBehaviour
{
    public Player_Control player_Control;
    public CharacterController characterController;
    bool hitwall;
    Vector3 velocity;
    [SerializeField] float climbSpeed = 2f;
    [SerializeField] Transform model;

    public float radius = 0.5f;
    public float distance = 1.0f;
    public LayerMask layerMask;

    public Animator anim;

    float h = 0f;
    float v = 0f;
    

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        player_Control = GetComponent<Player_Control>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        


        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 origin = model.position;
        Vector3 direction = transform.forward;

        // Effectuer le Spherecast
        RaycastHit hit;
        if (Physics.SphereCast(origin, radius, direction, out hit, distance, layerMask) && player_Control.Walk)
        {
            player_Control.climb = true;
            player_Control.gravity = 0f;
            Vector3 velocity2 = new Vector3(0, velocity.y, 0);
            
            characterController.Move(velocity2 * Time.fixedDeltaTime * 0.01f);
            anim.SetBool("climb", true);
        }
        else
        {
            player_Control.climb = false;
            player_Control.gravity = 30f;
            anim.SetBool("climb", false);
        }


    }

    private void OnAnimatorIK(int layerIndex)
    {
        velocity += anim.rootPosition;
    }




}
  
        




