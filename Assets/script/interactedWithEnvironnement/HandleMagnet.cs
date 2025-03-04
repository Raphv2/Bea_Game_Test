using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleMagnet : MonoBehaviour
{
    
    public float distanceMagnet;
    public LayerMask objectMagnetMask; 
    public bool touchObject;
    public bool activateMagnet = false;
    public bool setTransformOK = true;
    public GameObject player;
    public GameObject[] objectMagnet;
    public GameObject firePoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;
        RaycastHit hit;
        touchObject = Physics.Raycast(firePoint.transform.position, firePoint.transform.TransformDirection(Vector3.up), out hit, distanceMagnet, objectMagnetMask);
       

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            activateMagnet = !activateMagnet;

        }

        if (activateMagnet)
        {
            if (touchObject && hit.collider.tag == "objectMagnetTag" && setTransformOK)
            {
                objectMagnet[0] = hit.collider.gameObject;
                player.transform.position = objectMagnet[0].transform.position;
                player.transform.rotation = objectMagnet[0].transform.rotation;
                setTransformOK = false;
            }

            Rigidbody rigidbody = objectMagnet[0].GetComponent<Rigidbody>();

            rigidbody.useGravity = false;
            objectMagnet[0].transform.position = player.transform.position;
            objectMagnet[0].transform.rotation = player.transform.rotation;

            

        }
        else
        {
            objectMagnet[0].GetComponent<Rigidbody>().useGravity = true;
            player.transform.position = transform.position;
            player.transform.rotation = transform.rotation;
            objectMagnet[0] = null;
            setTransformOK = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && activateMagnet)
        {
            Rigidbody rigidbody = objectMagnet[0].GetComponent<Rigidbody>();
            rigidbody.AddForce(transform.TransformDirection(-Vector3.up) * 50000 * delta, ForceMode.Impulse);
            activateMagnet = false;
        }


    }
}
