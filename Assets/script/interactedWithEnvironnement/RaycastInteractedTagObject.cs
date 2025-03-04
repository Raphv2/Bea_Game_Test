using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastInteractedTagObject : MonoBehaviour
{
    public float distanceRaycast;
    public bool hitObject;
    public LayerMask layers;
    public string tagObject;
    public string nameObject;
    private void Awake()
    {
        
    }
    void Update()
    {
        RaycastHit hit;
        hitObject = Physics.Raycast(transform.position, transform.forward, out hit, distanceRaycast, layers);
        if (hitObject)
        {
            tagObject = hit.collider.gameObject.tag;
            nameObject = hit.collider.gameObject.name;
        }
        else 
        {
            nameObject = "";
            tagObject = "";
        }
       
    }
}
