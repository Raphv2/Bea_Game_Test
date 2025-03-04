using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireRotate : MonoBehaviour
{
    
    public Player_Control playerControlBea;
    public Transform transformRed;
    public Transform transformBlack;
    public Transform pivot;
    //public lancer lancer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControlBea.Punch)
        {
            //lancer.SpawnPrefab();
            transformRed.RotateAround(pivot.position, pivot.TransformDirection(Vector3.forward), 3);
            transformBlack.RotateAround(pivot.position, pivot.TransformDirection(Vector3.forward), -3);
        }

       
    }
}
