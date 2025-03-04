using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    
    public GameObject maskInstancier;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DestroyEnemy()
    {
        GameObject Instancier = Instantiate(maskInstancier, transform.localPosition, transform.localRotation);
        Instancier.GetComponent<Rigidbody>().AddForce(new Vector3(5,5,5), ForceMode.Impulse);
       
        Destroy(gameObject);
    }
}
