using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INVOCATION : MonoBehaviour
{
    public GameObject blobInvocation;
    public GameObject[] zoneInvocation;

    public void StartInvocation()
    {
        GameObject instancier;

        for (int i = 0; i < 4; i++)
        {
            instancier = Instantiate(blobInvocation, zoneInvocation[i].transform.position, zoneInvocation[i].transform.rotation);
        }
        
    }
}
