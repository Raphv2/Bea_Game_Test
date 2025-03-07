using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKlegController : MonoBehaviour
{
    Animator anim;

    public LayerMask layerMask; 

    [Range(0, 10f)]
    public float DistanceToGround; 

    private void Start()
    {

        anim = GetComponent<Animator>();

    }


    private void OnAnimatorIK(int layerIndex)
    {

        if (anim)
        { 
           anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);
            anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
            anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);

            // Left Foot
            RaycastHit hit;
            
            Ray ray = new Ray(anim.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
            if (Physics.Raycast(ray, out hit, DistanceToGround + 1f, layerMask))
            {

                
                if (hit.transform.tag == "Walkable")
                {

                    Vector3 footPosition = hit.point; 
                    footPosition.y += DistanceToGround; 
                    anim.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
                    anim.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, hit.normal));

                }

            }

            // Right Foot
            ray = new Ray(anim.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);
            if (Physics.Raycast(ray, out hit, DistanceToGround + 1f, layerMask))
            {

                if (hit.transform.tag == "Walkable")
                {
                    
                    Vector3 footPosition = hit.point;
                    footPosition.y += DistanceToGround;
                    anim.SetIKPosition(AvatarIKGoal.RightFoot, footPosition);
                    anim.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, hit.normal));

                }

            }


        }

    }

}


