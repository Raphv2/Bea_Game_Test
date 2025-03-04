using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleDistributeur : MonoBehaviour
{
    public Animation_Player animationPlayer;
    public SwitchObjects switchObjects;
    public IsInteracted isInteracted;
    public Transform beaTransform;
    public Transform target;
    
    
    void Start()
    {
        beaTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animationPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Animation_Player>();
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;
        if (isInteracted.isInteracted)
        {
           animationPlayer.pressButton = true;
            beaTransform.rotation = Quaternion.LookRotation(-transform.position);
            beaTransform.position = target.position;
            switchObjects.selectedNumber = 1;
        }

      

        
    }
}
