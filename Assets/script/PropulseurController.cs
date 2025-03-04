using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropulseurController : MonoBehaviour
{
    [SerializeField]
    private GameObject firePoint;
    public GameObject instanciateObject;
    GameObject instancier;

    public SwitchObjects switchObjects;

    public Animation animationTurbine;

    public float loadingChargeTime;
    public float chargeTimeMax;
    public float chargeTimeMin;
    public float timeTornado;
    void Start()
    {
        animationTurbine.enabled = false ;
    }

    // Update is called once per frame
    void Update()
    {
        if(switchObjects.selectedNumber == 4) HandleCharge();
    }


    private void HandleCharge()
    {
        bool fireDown = Input.GetMouseButton(0);
        bool fireUp = Input.GetMouseButtonUp(0);
        float delta = Time.deltaTime;

        if (fireDown && loadingChargeTime <= chargeTimeMax)
        {
            loadingChargeTime += delta;
            animationTurbine.enabled = true;
        }


        if (fireUp && loadingChargeTime > chargeTimeMin && instancier == null)
        {
            instancier = Instantiate(instanciateObject, firePoint.transform.position, firePoint.transform.rotation);
            instancier.transform.parent = transform;
            instancier.transform.localScale = new Vector3(instancier.transform.localScale.x, instancier.transform.localScale.y, instancier.transform.localScale.y * loadingChargeTime);
            loadingChargeTime = 0f;
            animationTurbine.enabled = false;
        }
        if (instancier != null) Destroy(instancier, timeTornado );
    }   
}
