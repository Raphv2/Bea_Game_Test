using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInteracted : MonoBehaviour
{
    // programme qui sert à l'interaction du joueur à lobjet
    public Player_Control playerControl;
    public Transform target;
    public Transform pointInteracted;
    public bool isInteracted = false;
    public float distanceTarget;
    public float distanceminimale;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Control>();
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
        distanceTarget = Vector3.Distance(target.transform.position, pointInteracted.position);
        if (distanceTarget < distanceminimale && Input.GetKeyDown(KeyCode.E))
        {
            isInteracted = true;
        }
        else isInteracted = false;

        

    }

    
}
