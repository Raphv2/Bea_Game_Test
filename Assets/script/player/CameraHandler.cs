using UnityEngine;
using UnityEngine.UI;



public class CameraHandler : MonoBehaviour
{
    public SwitchObjects switchObjects;
    public SkinnedMeshRenderer skinBea;
    public bool lockTarget;
        
    public bool reset;
    public bool lockTargetBool;

    public int lockTragetInt = 1;
    public Vector3 aimPosition;
        

    public GameObject[] tabGameObjectsEnemy = new GameObject[100];
    public int tabGameObjectsEnemyLenght;
    public bool instancierOk;
    public bool enterSafeZone;
    public GameObject enemyLock;
    public GameObject prefab;
    GameObject lockPoint;
    public float distanceLock;

    public Transform transformBea;
    public Transform cameraTransformInitial;
    public Transform targetTransform;
    public Transform cameraTransform;
    public Transform cameraPivotTransform;
    private Transform myTransform;
    private Vector3 cameraTransformPosition;
        
    private Vector3 cameraFollowVelocity = Vector3.zero;

    public static CameraHandler singleton;
    public Player_Control playerControl; 

    public float lookSpeed = 0.1f;
    public float followSpeed = 0.1f;
    public float pivotSpeed = 0.1f;
    public float speedCamDelta = 0.1f;

    private float targetPosition;
    private float defaultPosition;
    public float aimDefaultPosition;
    public float lookAngle;
    public float pivotAngle;
    public float minimumPivot = -90;
    public float maximumPivot = 35;

    public float cameraShereRadius = 0.2f;
    public float cameraCollisionOffset = 0.2f;
    public float minimumCollisonOffset = 0.1f;

    public LayerMask entity;
    private Vector3 offset;
    private void Awake()
    {

            
        singleton = this;
        myTransform = transform;
        defaultPosition = cameraTransform.localPosition.z;
        aimPosition = new Vector3(3f, 0f, 10f);
    }

    private void Start()
    {
    offset = cameraTransform.position - myTransform.position;
        transformBea = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        switchObjects = GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchObjects>();
    }

    private void Update()
        {
            float delta = Time.deltaTime;
            float MouseX = Input.GetAxis("Mouse X");
            float MouseY = Input.GetAxis("Mouse Y");
            float vertical = Input.GetAxis("Vertical");

            //HandleCameraCollisions(delta);

            FollowTarget(delta);

            Lock(delta , vertical);

            HandleCameraRotation(delta, MouseX, MouseY);

            AimTarget();
                
        }

    public void FollowTarget(float delta)
    {
        Vector3 targetPosition = Vector3.SmoothDamp(myTransform.position, targetTransform.position, ref cameraFollowVelocity, delta / followSpeed);
        myTransform.position = targetPosition;

        HandleCameraCollisions(delta);
    }

    public void HandleCameraRotation(float delta, float mouseXInput, float mouseYinput)
    {
        if (lockTargetBool)
        {
            return;
        }
        lookAngle += (mouseXInput * lookSpeed) / delta;
        pivotAngle -= (mouseYinput * pivotSpeed) / delta;
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);
            
        if(Mathf.Abs(lookAngle) > 360)
        {
            lookAngle = 0;
        }

        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        myTransform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = -pivotAngle;
            
        targetRotation = Quaternion.Euler(rotation);
        cameraPivotTransform.localRotation = targetRotation;
    }

    private void HandleCameraCollisions(float delta)
    {
        if (Input.GetKey(KeyCode.Mouse1) || enterSafeZone)
        {
            targetPosition = aimDefaultPosition;
        }

        else targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
        direction.Normalize();


        if (Physics.SphereCast(cameraPivotTransform.position, cameraShereRadius, direction, out hit, Mathf.Abs(targetPosition), entity))
        {
            float dis = Vector3.Distance(cameraPivotTransform.position, hit.point);
            targetPosition = (dis - cameraCollisionOffset);
        }

        if (Mathf.Abs(targetPosition) < minimumCollisonOffset)
        {
            targetPosition = -minimumCollisonOffset;
        }

        cameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, delta / 0.1f);
        cameraTransform.localPosition = cameraTransformPosition;
    }
    public void Lock(float delta, float vertical)
    {
            

        if (lockTarget && Input.GetMouseButton(2))
        {
            Destroy(lockPoint);
            lockTargetBool = true;
                
            tabGameObjectsEnemy = GameObject.FindGameObjectsWithTag("enemy");
            tabGameObjectsEnemyLenght = tabGameObjectsEnemy.Length;
            lockTragetInt++;
            lockTarget = false;
            if (instancierOk)
            {
                lockPoint = Instantiate(prefab, tabGameObjectsEnemy[lockTragetInt -1].transform.position + new Vector3(0, 10f, 0), tabGameObjectsEnemy[lockTragetInt -1].transform.rotation);
                lockPoint.transform.parent = tabGameObjectsEnemy[lockTragetInt -1 ].transform;
                instancierOk = false;

            }

        }

        else if (!Input.GetMouseButton(2))
        {
            lockTarget = true;
            instancierOk = true;
        }

        if (lockTragetInt > tabGameObjectsEnemyLenght)
        {
            lockTragetInt = 0;
            lockTargetBool = false;
            Vector3 direction = cameraPivotTransform.position - cameraTransform.position;
            cameraTransform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }


        if (Input.GetKey(KeyCode.U) || !lockTargetBool || (vertical < 0 && playerControl.Sprint))
        {
            Destroy(lockPoint);
            lockTargetBool = false;
            Vector3 direction = cameraPivotTransform.position - cameraTransform.position;
            cameraTransform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            //Destroy(gameObject);
            instancierOk = true;
        }


        if (lockTargetBool)
        {
            //enemyLock = GameObject.FindGameObjectWithTag("enemy");
            distanceLock = Vector3.Distance(transformBea.position, tabGameObjectsEnemy[lockTragetInt-1].transform.position);
            if (distanceLock > 5f && distanceLock < 100f)
            {
                Vector3 direction = transformBea.position - tabGameObjectsEnemy[lockTragetInt-1].transform.position;
            if (!playerControl.Walk)
            {
                transformBea.rotation = Quaternion.LookRotation(new Vector3(-direction.x, 0, -direction.z));
            }
                    
                cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, Quaternion.LookRotation(-direction), delta / 1);
                cameraPivotTransform.rotation = Quaternion.Slerp(cameraPivotTransform.rotation, Quaternion.LookRotation(direction),delta/1 );
            }
            else lockTargetBool = false;


        }
    }
        
    
        
    public void AimTarget()
    {
        Vector3 rotateBea;
        if (Input.GetKey(KeyCode.Mouse1))
        {
            
            cameraTransform.localPosition = aimPosition;
            rotateBea = cameraTransform.rotation.eulerAngles;
            rotateBea.x = 0f;
            rotateBea.z = 0f;
            transformBea.rotation = Quaternion.Euler( rotateBea);
        }
        else if(enterSafeZone)
        {
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, aimPosition, Time.deltaTime /5f);
        }


    }
        
}