using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBea : MonoBehaviour
{
    public Transform beaTransform;
    // Start is called before the first frame update
    void Start()
    {
        beaTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = beaTransform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, 0);
        transform.rotation = rotation;
    }
}
