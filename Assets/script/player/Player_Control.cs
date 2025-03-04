using UnityEngine;
using UnityEngine.UI;

public class Player_Control : MonoBehaviour
{



    Vector3 distanceInitial;
    Vector3 normalVector;
    Vector3 velocity;
    Vector3 rootMotion;
    [Header("Stat")]

    public float stamina = 100f;
    public float health = 100f;
    public float mana = 100f;
    public float distanceray = 1;
    public float stepDown;

    public Image healthBarImage;
    public Image staminaBarImage;
    public Image manaBarImage;
    public Image staminaDelayBar;

    float timerStamina;

    [Header("movement")]

    public string inputSneak;

    public float moveAmount;

    public float timerFalling;
    public float timerGrounded;

    public float timerRoll;

    public float gravity;
    public float jumpHeight;

    [Header("bool de condition")]

    public bool Sprint = false;
    public bool Walk = false;
    public bool Jump = false;
    public bool climb;

    public bool Sneak = false;
    public bool Roulade = false;
    public bool Roulade_ok = true;
    public bool plafond;

    public bool Punch = false;

    public bool inAir = false;
    public bool falling = false;
    public bool fallingDeath = false;
    public bool fallingLanding = false;
    public bool timerOk = false;


    [Header("component")]

    public CharacterController characterController;

    public Transform mainCamera;

    public GameObject rayplafond;

    public GameObject Bea;

    public CameraHandler cameraHandler;

    public Animation_Player animationPlayer;

    [SerializeField]

    private LayerMask Layer_escalade;

    private void Awake()
    {
        Cursor.visible = false;
    }

    void Start()
    {
        mainCamera = Camera.main.transform;

        cameraHandler = GameObject.FindGameObjectWithTag("camera").GetComponent<CameraHandler>();

        characterController = GetComponent<CharacterController>();

        animationPlayer = GetComponent<Animation_Player>();
    }


    void Update()
    {


        healthBarImage.fillAmount = health / 100;
        staminaBarImage.fillAmount = stamina / 100;
        manaBarImage.fillAmount = mana / 100;

        float delta = Time.deltaTime;
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            HandleJump(delta);
        }

        Punch = Input.GetMouseButton(0);

        Roulade = Input.GetButtonDown("Fire2");

        Sneak = Input.GetKey(inputSneak);

        RaycastHit hitplafond;

        plafond = Physics.Raycast(rayplafond.transform.position, rayplafond.transform.TransformDirection(Vector3.up), out hitplafond, 3);

        HandleRoll(delta, vertical, horizontal);

    }

    private void OnAnimatorMove()
    {
        rootMotion += animationPlayer.Anim.deltaPosition;
        
    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        StaminaDelay(delta);
        Life();
        HandleFalling(delta);     
        HandleLocomotion(delta, vertical, horizontal);       
    }

    private void HandleLocomotion(float delta, float vertical, float horizontal)
    {
        if (Roulade || animationPlayer.pressButton || animationPlayer.climbEchelle)
        {
            return;

        }

        if (vertical != 0 || horizontal != 0 && !inAir)
        {
            Walk = true;
            moveAmount = 1;


            if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
            {
                Sprint = true;
                stamina -= 5f * delta;
            }
            else Sprint = false;

        }
        else
        {
            moveAmount = 0;
            Walk = false;
            Sprint = false;
        }

        

        if (Walk)
        {

            HandleRotation(delta, vertical, horizontal);

        }

        

        if (Jump)//dans les air
        {
            velocity.y -= gravity * delta;
            characterController.Move(velocity * delta);
            Jump = !characterController.isGrounded;
            rootMotion = Vector3.zero;
        }
        else //au sol
        {
           
            characterController.Move(rootMotion + Vector3.down * stepDown);

            rootMotion = Vector3.zero;

            if (!characterController.isGrounded && !Jump)
            {
                characterController.Move(velocity * delta * 0.1f);
                
                rootMotion = Vector3.zero;
                velocity.y -= gravity * delta;
                velocity = animationPlayer.Anim.velocity;
                velocity.y = 0;

            }


        }
            
    }

    void HandleJump(float delta)
    {
        if (!Jump)
        {
            Jump = true;
            velocity = animationPlayer.Anim.velocity;
            velocity.y = Mathf.Sqrt(2 * gravity * jumpHeight);
        }
    }

    public void HandleRotation(float delta, float vertical, float horizontal)
    {

        Vector3 targetDir = Vector3.zero;
        float moveOverride = moveAmount;

        targetDir = mainCamera.forward * vertical;
        targetDir += mainCamera.right * horizontal;

        targetDir.Normalize();
        targetDir.y = 0;

        if (targetDir == Vector3.zero) targetDir = transform.forward;

        float rs = 7f;

        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rs * delta);


        transform.rotation = targetRotation;
    }

    private void HandleRollRotation(float delta, float vertical, float horizontal)
    {
        Vector3 targetDirRoll = Vector3.zero;


        targetDirRoll = mainCamera.forward * vertical;
        targetDirRoll += mainCamera.right * horizontal;

        targetDirRoll.Normalize();
        targetDirRoll.y = 0;

        if (targetDirRoll == Vector3.zero) targetDirRoll = transform.forward;

        float rs = 300f;

        Quaternion tr = Quaternion.LookRotation(targetDirRoll);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rs * delta);

        transform.rotation = targetRotation;
    }

    private void HandleRoll(float delta, float vertical, float horizontal)
    {
        if (animationPlayer.pressButton || Sneak)
        {
            return;
        }

        if (Walk && Roulade)
        {

            if (stamina >= 20)
            {
                Bea.layer = 2;
                HandleRollRotation(delta, vertical, horizontal);
                Roulade_ok = false;
                animationPlayer.Anim.SetBool("roll", true);
            }

            

        }
        else animationPlayer.Anim.SetBool("roll", false);

    }

    private void HandleFalling(float delta)
    {
        if (animationPlayer.climbEchelle)
        {
            return;
        }

        RaycastHit hitDistance;
        bool Distance = Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hitDistance);
        float distanceRay = hitDistance.distance;
        Vector3 distanceFalling = Vector3.zero;
        distanceFalling = distanceInitial - new Vector3(0, hitDistance.distance, 0);


        Vector3 targetDirfall = Vector3.zero;
        float moveOverride = moveAmount;



        if (distanceFalling.y < 0)
        {
            distanceFalling.y = 0;
        }


        inAir = !characterController.isGrounded;

        if (inAir)
        {
            timerOk = true;
            timerFalling += delta;
            

        }
        else
        {
            
            timerFalling = 0;
        }
            

        

        if (characterController.isGrounded && timerOk)
        {
            timerGrounded += delta;
            distanceInitial = new Vector3(0, distanceRay, 0);
        }
        else timerGrounded = 0;

        if (timerFalling > 0.4f)
        {
            
            falling = true;
            if (!Jump && !climb)
            {
                animationPlayer.Anim.SetBool("falling", true);
            }
            if (timerFalling < 0.41f)
            {

                distanceInitial = new Vector3(0, distanceRay, 0);
            }

        }

        else
        {
            animationPlayer.Anim.SetBool("falling", false);
            falling = false;
            fallingDeath = false;
        }

        if (timerGrounded < 0.05f && timerGrounded > 0f)
        {

            if (distanceFalling.y > 15f)
            {
                health -= Mathf.FloorToInt(((100 * distanceFalling.y) / 40f) * delta * 30);
                
            }



            if (health > 0f && distanceFalling.y > 10f)
            {
                fallingLanding = true;
                animationPlayer.Anim.SetTrigger("falling_landing");
            }

            if (health == 0)
            {
                fallingDeath = true;
                animationPlayer.Anim.SetTrigger("falling_death");
            }
        }
        else
        {
            fallingDeath = false;
            fallingLanding = false;
        }

        if (timerGrounded > 1f)
        {

            timerOk = false;
            distanceFalling.y = 0;

        }


    }

    private void StaminaDelay(float delta)
    {


        if (!Sprint && !Roulade && stamina < 100)
        {
            staminaDelayBar.fillAmount = timerStamina * 100 / 100;
            timerStamina += delta;
        }
        else
        {
            timerStamina = 0;
            staminaDelayBar.fillAmount = 1f;
        }

        if (timerStamina > 1 && stamina < 100)
        {
            stamina++;
        }


    }

   

    private void Life()
    {
        if (health == 0)
        {
            fallingDeath = true;

        }

        if (health < 0)
        {
            health = 0;
        }
    }

    private void RollStamina()
    {
        stamina -= 20;
    }

    private void RollInvisible()
    {
        Bea.layer = 2;
    }

    private void SetLayer()
    {
        Bea.layer = 6;
    }

}
