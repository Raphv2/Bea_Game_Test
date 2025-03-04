using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class castspell : MonoBehaviour
{
    public GameObject prefab;
    public GameObject spawnPoint;
    
    public Transform beaTransform;
    private GameObject instancier;

    
    
    private void Start()
    {

    }


    void CastSpellSpawn()
    {
        instancier = Instantiate(prefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        instancier.transform.parent = spawnPoint.transform;
    }

    void SpellLancer()
    {
        Vector3 direction = beaTransform.position - transform.position;
        float delta = Time.deltaTime;
        instancier.transform.parent = null;
        instancier.GetComponent<Rigidbody>().AddForce(direction * 200 * delta, ForceMode.Impulse);
    }
}