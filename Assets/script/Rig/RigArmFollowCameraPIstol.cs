using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class RigArmFollowCameraPIstol : MonoBehaviour
{
    
   
    public SwitchObjects switchObjects;
    public Rig rigArm;
    public Rig rigHead;
    public Rig rigPoseArmed;
    public Rig rigPoseUnarmed;
    public GameObject Laser;

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;
        if (switchObjects.selectedNumber == 2 && switchObjects.objectsToActivate[1].name != "default")
        {
            rigArm.weight = Mathf.Lerp(rigArm.weight, 0.7f, delta * 10);
            rigHead.weight = Mathf.Lerp(rigHead.weight, 1f, delta * 10);
            rigPoseArmed.weight = Mathf.Lerp(rigPoseArmed.weight, 1f, delta * 10);
            rigPoseUnarmed.weight = Mathf.Lerp(rigPoseUnarmed.weight, 0f, delta * 10);
            Laser.SetActive(true);
        }
        else
        {
            rigArm.weight = Mathf.Lerp(rigArm.weight, 0f, delta * 10);
            rigHead.weight = Mathf.Lerp(rigHead.weight, 0f, delta * 10);
            rigPoseArmed.weight = Mathf.Lerp(rigPoseArmed.weight, 0f, delta * 10);
            rigPoseUnarmed.weight = Mathf.Lerp(rigPoseUnarmed.weight, 1f, delta * 10);
            Laser.SetActive(false);
        }
        
    }
}
