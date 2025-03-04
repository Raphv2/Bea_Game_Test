using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lancer : MonoBehaviour
{
    public GameObject prefab;
    public Transform spawnPoint;
    public Player_Control beaPlayerControl;

    
    
    private void Start()
    {
        
    }

    
    void SpawnPrefab()
    {
        float delta = Time.deltaTime;

            if(beaPlayerControl.mana >= 10) 
            { 
                GameObject newPrefab = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
                newPrefab.GetComponent<Rigidbody>().AddForce(spawnPoint.TransformDirection(Vector3.forward) * 500000 * delta, ForceMode.Force);
                
                beaPlayerControl.mana -= 10;
            }



    }
}
