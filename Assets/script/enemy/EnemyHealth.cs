
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Image healthBarImage;
    public float health = 100f;
    public float healthMax = 100f;

    
    public bool death = false;
    public Animator anim;
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBarImage.fillAmount = health / healthMax;

        if (healthBarImage.fillAmount > 0) return;

        if(healthBarImage.fillAmount <= 0)
        {
            anim.SetBool("death", true);
            death = true;
        }
        
    }
}
