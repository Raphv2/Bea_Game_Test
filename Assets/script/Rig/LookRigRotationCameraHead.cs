using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class LookRigRotationCameraHead : MonoBehaviour
{
    public GameObject target;
    public GameObject pivot;
    public CameraHandler cameraHandler;
    public GameObject aimObject;
    public float minimumPivot = -35f;
    public float maximumPivot = 35f;
   // public Canvas canvaMire;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;


        Vector3 direction = aimObject.transform.position - cameraHandler.cameraPivotTransform.position;
        
        
        target.transform.rotation = cameraHandler.cameraTransform.rotation;
        Vector3 rotationTarget = target.transform.rotation.eulerAngles;

        
        Vector3 rotationPivotEuler = pivot.transform.rotation.eulerAngles;
        Vector3 rotationPivot = new Vector3(0,  rotationPivotEuler.y, 0);
         Quaternion pivotRotation = Quaternion.Euler(rotationPivot); 
         Quaternion ballRotation = Quaternion.Euler(rotationTarget); 

         
         float angle = Quaternion.Angle(ballRotation, pivotRotation);

         float adjustedAngle = (angle);

        


        if (adjustedAngle >= minimumPivot && adjustedAngle  <= maximumPivot)
         {

            target.transform.position = transform.position + direction;
        } 
    }
}
