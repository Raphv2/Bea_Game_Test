using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class punch : MonoBehaviour
{
    [SerializeField] Player_Control Pc;
    public BoxCollider Bc;
    // Start is called before the first frame update
    void Start()
    {
        Bc = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Pc.Punch)
        {
            Bc.enabled = true;
        }
        else Bc.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        //if (other.CompareTag("enemy") && Input.GetMouseButton(0))
        //{
        other.gameObject.transform.Translate(new Vector3(10, 0, 0));

        //}

    }
}
