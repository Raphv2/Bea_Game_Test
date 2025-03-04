using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class PickUpWeapon : MonoBehaviour
{
    public RigBuilder rigBuilder;
    public RaycastInteractedTagObject raycastInteractedTagObject;
    public SwitchObjects switchObjects;
    public GameObject[] tabObjects;
    public MultiPositionConstraint[] multiPositionConstraints;
    public MultiParentConstraint[] multiParentConstraints;
    public TwoBoneIKConstraint[] twoBoneIKConstraints;
    public MultiAimConstraint[] multiAimConstraints;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!Input.GetKeyDown(KeyCode.E) || !raycastInteractedTagObject.hitObject)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.E) && raycastInteractedTagObject.hitObject)
        {
            




            for (int i = 0; i < tabObjects.Length; i++)
            {

                

                if (raycastInteractedTagObject.nameObject == tabObjects[i].name)
                {
                    tabObjects[i].gameObject.SetActive(true);
                    if (raycastInteractedTagObject.tagObject == "meleeWeapon")
                    {
                        switchObjects.objectsToActivate[2].SetActive(false);
                        switchObjects.objectsToActivate[2] = tabObjects[i].gameObject;

                    }
                    else if (raycastInteractedTagObject.tagObject == "gunWeapon")
                    {


                        switchObjects.objectsToActivate[1].SetActive(false);
                        switchObjects.objectsToActivate[1] = tabObjects[i].gameObject;

                        twoBoneIKConstraints[0].data.target = tabObjects[i].gameObject.transform.Find("gripLeft");
                        twoBoneIKConstraints[1].data.target = tabObjects[i].gameObject.transform.Find("gripRight");

                        multiPositionConstraints[0].data.constrainedObject = tabObjects[i].gameObject.transform;
                        
                        multiParentConstraints[0].data.constrainedObject = tabObjects[i].gameObject.transform;

                        multiPositionConstraints[1].data.constrainedObject = tabObjects[i].gameObject.transform;

                        multiParentConstraints[1].data.constrainedObject = tabObjects[i].gameObject.transform;

                        multiAimConstraints[0].data.constrainedObject = tabObjects[i].gameObject.transform;

                        

                    }
                    else if (raycastInteractedTagObject.tagObject == "rifleWeapon")
                    {
                        switchObjects.objectsToActivate[3].SetActive(false);
                        switchObjects.objectsToActivate[3] = tabObjects[i].gameObject;

                        
                        twoBoneIKConstraints[2].data.target = tabObjects[i].gameObject.transform.Find("gripLeft");
                        twoBoneIKConstraints[3].data.target = tabObjects[i].gameObject.transform.Find("gripRight");

                        multiPositionConstraints[2].data.constrainedObject = tabObjects[i].gameObject.transform;

                        multiParentConstraints[2].data.constrainedObject = tabObjects[i].gameObject.transform;

                        multiPositionConstraints[3].data.constrainedObject = tabObjects[i].gameObject.transform;

                        multiParentConstraints[3].data.constrainedObject = tabObjects[i].gameObject.transform;

                        multiAimConstraints[1].data.constrainedObject = tabObjects[i].gameObject.transform;

                        

                        
                    }

                }


                




            }
            rigBuilder.Build();


        }

        


    }
}
