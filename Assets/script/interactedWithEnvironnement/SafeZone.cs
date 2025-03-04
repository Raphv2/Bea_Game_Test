using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public CameraHandler cameraHandler;

    private void Awake()
    {
        cameraHandler = GameObject.FindGameObjectWithTag("camera").GetComponent<CameraHandler>();
    }
    private void OnTriggerEnter(Collider other)
    {
        cameraHandler.enterSafeZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        cameraHandler.enterSafeZone = false;
    }
}
