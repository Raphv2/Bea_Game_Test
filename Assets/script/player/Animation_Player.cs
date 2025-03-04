using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations.Rigging;
using UnityEditor;

public class Animation_Player : MonoBehaviour
{
    
    public TrailRenderer trailRenderer;
    public DamageHammer damageHammer;
    public bool HammerOk = true;
    public bool isAttacking = true;
    public bool pressButton = false;
    public bool climbEchelle = false;
    public Animator Anim;
    public Player_Control PC;
    public CameraHandler cameraHandler;
    public SwitchObjects switchObjects;
    float timerPunch;
    int random = 999;
    public Rig meleeWeaponRigArmed;
    public Rig meleeWeaponRigUnarmed;

    void Start()
    {
        Anim = GetComponent<Animator>();
        
}

    
    void Update()
    {
        float delta = Time.deltaTime;
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        


        if (switchObjects.selectedNumber == 1 && !climbEchelle)
        {
            Anim.SetLayerWeight(1, 1f);
            Anim.SetLayerWeight(2, 0f);
        }

        if (switchObjects.selectedNumber == 2 && switchObjects.objectsToActivate[1].name != "default")
        {

            Anim.SetLayerWeight(2, 1f);




            if (PC.Punch && PC.mana >= 10)
            {

                Anim.SetBool("Punch", true);


            }
            else
            {

                Anim.SetBool("Punch", false);
            }

        }
        else
        {
            Anim.SetLayerWeight(2, 0f);
        }
        

        if (switchObjects.selectedNumber == 3 && switchObjects.objectsToActivate[2].name != "default")
        {
            Anim.SetLayerWeight(2, Mathf.Lerp(Anim.GetLayerWeight(2), 0f, delta * 5));
            Anim.SetLayerWeight(7, Mathf.Lerp(Anim.GetLayerWeight(7), 1f, delta * 5));
            meleeWeaponRigArmed.weight = Mathf.Lerp(meleeWeaponRigArmed.weight, 1f, delta * 5);
            meleeWeaponRigUnarmed.weight = Mathf.Lerp(meleeWeaponRigUnarmed.weight, 0f, delta * 5);
            if (HammerOk)
            {
                Anim.Play("sortir hammer");
                HammerOk = false;
            }

            if (PC.Punch)
            {



                if (isAttacking)
                {
                    random = Random.Range(0, 2);
                    isAttacking = false;
                }


                if (random == 0 && !isAttacking)
                {
                    Anim.SetTrigger("slash 1");
                    random = 999;
                }

                if (random == 1 && !isAttacking)
                {
                    Anim.SetTrigger("slash 2");
                    random = 999;
                }
            }
            else
            {

                Anim.SetBool("Punch", false);
            }

        }
        

        if (switchObjects.selectedNumber == 4 && switchObjects.objectsToActivate[3].name != "default")
        {

            Anim.SetLayerWeight(8, 1f);
            Anim.SetLayerWeight(2, 0f);



            if (PC.Punch && PC.mana >= 10)
            {

                Anim.SetBool("Punch", true);


            }
            else
            {

                Anim.SetBool("Punch", false);
            }

        }
        else Anim.SetLayerWeight(8, Mathf.Lerp(Anim.GetLayerWeight(8), 0f, delta * 5));




        if (switchObjects.selectedNumber != 3 && switchObjects.objectsToActivate[2].name != "default") 
        {
            HammerOk = true;
            
            Anim.SetLayerWeight(7, Mathf.Lerp(Anim.GetLayerWeight(7), 0f, delta * 5));

            meleeWeaponRigArmed.weight = Mathf.Lerp(meleeWeaponRigArmed.weight, 0f, delta * 5);
            meleeWeaponRigUnarmed.weight = Mathf.Lerp(meleeWeaponRigUnarmed.weight, 1f, delta * 5);
        }

       


        if (!PC.Roulade || !cameraHandler.lockTargetBool)
        {
            Anim.SetBool("walk", PC.Walk);
            Anim.SetBool("run", PC.Sprint);

        }

        Anim.SetBool("jump", PC.Jump );

        if (PC.Sneak)
        {
            Anim.SetLayerWeight(3, Mathf.Lerp(Anim.GetLayerWeight(3), 1f, delta * 10 ));
            
                
        }
        else if(!PC.Sneak && !PC.plafond)
        {
            
            Anim.SetLayerWeight(3, Mathf.Lerp(Anim.GetLayerWeight(3), 0f, delta * 10));
        }

        if (pressButton)
        {
            Anim.SetLayerWeight(5, 1f);
            Anim.SetTrigger("interacted");
        }
        else Anim.SetLayerWeight(5, 0f);

        
    }

    public void Interuption()
    {
        pressButton = false;
        PC.health = 100;
    }

    public void IsAttacking()
    {
        isAttacking = true;

        damageHammer.damage = true;
        
    }

    
    

    


}
