using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class RigControlHead : MonoBehaviour
{
    public SwitchObjects switchObjects;
    public Rig rigHead;
    

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;
        if (switchObjects.selectedNumber == 1 || switchObjects.objectsToActivate[1].name == "default" || switchObjects.objectsToActivate[2].name == "default" || switchObjects.objectsToActivate[3].name == "default")
        {

            rigHead.weight = Mathf.Lerp(rigHead.weight, 1f, delta * 10);

        }
        else
        {

            rigHead.weight = Mathf.Lerp(rigHead.weight, 0f, delta * 10);
        } 
    }
}
