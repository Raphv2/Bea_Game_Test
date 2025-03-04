using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDirectionTarget : MonoBehaviour
{
    public Transform transformBea;

    private void Awake()
    {
        transformBea = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = transformBea.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
