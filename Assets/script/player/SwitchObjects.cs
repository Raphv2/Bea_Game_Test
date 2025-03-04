using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchObjects : MonoBehaviour
{
    public GameObject[] objectsToActivate;
    public int selectedNumber;

    void Start()
    {
        // Désactive tous les Game Objects au départ
        //foreach (GameObject obj in objectsToActivate)
        //{
        //    obj.SetActive(false);
        //}
        selectedNumber = 1;
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedNumber = 1;
            ActivateObjects();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedNumber = 2;
            ActivateObjects();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedNumber = 3;
            ActivateObjects();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedNumber = 4;
            ActivateObjects();
        }


    }

    void ActivateObjects()
    {
        
        //foreach (GameObject obj in objectsToActivate)
        //{
        //    obj.SetActive(false);
        //}

        // Active le Game Object correspondant au chiffre sélectionné
        if (selectedNumber == 1)
        {
            objectsToActivate[0].SetActive(true);
        }
        else if (selectedNumber == 2)
        {
            objectsToActivate[1].SetActive(true);
        }
        else if (selectedNumber == 3)        {
            objectsToActivate[2].SetActive(true);
        }
        else if (selectedNumber == 4)
        {
            objectsToActivate[3].SetActive(true);
        }

        // Ajouter des conditions supplémentaires pour d'autres chiffres si nécessaire
    }
}
