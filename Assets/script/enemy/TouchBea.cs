using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchBea : MonoBehaviour
{

    public Player_Control beaPlayerControl;

    private void Awake()
    {
        beaPlayerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Control>();
    }

    private void Update()
    {

        Destroy(gameObject, 5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") )
        {
            Destroy(gameObject);
            beaPlayerControl.health -= 10;
        }
        if (other.gameObject)
        {
            Destroy(gameObject);
        }

    }

}
