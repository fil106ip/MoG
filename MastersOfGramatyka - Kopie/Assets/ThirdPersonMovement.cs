using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Cinemachine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private Transform player, respawnPoint;
    [SerializeField] private float slopeForce, slopeForceRayLength;

    public CharacterController controller;
    public Transform cam;
    
    private float speed = 6f; //die addition von speed und sprint ergibt die eigentliche sprint speed also 12m/s
    private readonly float sprint = 6f; //sprint is 6m/s, die zu dem base speed hinzugefügt werden
    private readonly float baseSpeed = 6f; //base speed is 6m/s
    private readonly float sneak = 3f; //sneak speed is 3m/s

    private readonly float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    [SerializeField] private Vector3 originalCenter, reducedCenter, offsetUp;

    private float originalHeight = 3.14f;
    private readonly float reducedHeight = 2f;

    private bool sprinting, crouching; //damit wir unseren spieler mit dem bool sprinting flaggen können, betrifft auch crouching und sneaking.
    public bool zoomed;

    public CharacterStats charStats;
    public HealthBar healthBar;
    public Jump jump;

    public float targetAngle, angle;

    [SerializeField] private GameObject freeLook;

    public CinemachineFreeLook freeLookCam;
    public CinemachineVirtualCamera vCam;

    public int maxStamaina = 100; //die ausdauer wird auf 100 bestimmt
    public int currentStamaina; //unsere current ausdauer
    private Coroutine stamainaUsing, stamainaRegen, stamainaRegencrouch; /*die coroutine vom IEnumerator wird initialisert, 
                                                                            da sie gestoppt werden muss. Wird im weiteren verlauf des codes noch gezeigt*/
    private Animator anim; //für die animationen
    public StamainaBar stamBar; //für die stamina bar

    public LayerMask ignoreMe;
    public GameObject bow;


    void Start()
    {
        originalHeight = controller.height; //am anfang wird festgelegt, dass die originalhöhe der controller höhe entspricht
        currentStamaina = maxStamaina; //damit unsere current ausdauer der max ausdauer entspricht wenn man das game beginnt
        anim = GetComponent<Animator>(); //für animationen
        anim.SetBool("isWalking", false); //unser spieler ist immer idle am anfang wenn das game beginnt. Er ist nur idle solange isWalking false ist.
        anim.SetBool("isSprinting", false);
        anim.SetBool("isCrouching", false);
    }


    void Update()
    {

        if (vCam.Priority > freeLookCam.Priority)
        {
            bow.SetActive(true);
            GetUp();
        }
        else if (vCam.Priority < freeLookCam.Priority)
        {
            bow.SetActive(false);
        }

        Walking();  //methode fürs laufen, wird weiter unten genauer erläutert
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        if (Input.GetButtonDown("Sneak") && !zoomed) //wurde links strg gedrückt?
        {
                if (!crouching && freeLookCam.Priority > vCam.Priority) //wenn er nicht crouched darf er crouchen.
                {
                    Crouch();
                
                }
                else //wenn er crouched und nochmal links strg gedrückt wurde dann steht der char auf und er hat die normale geschwindigkeit wieder
                {
                    GetUp();
                    NormalSpeed();
                }
        }
        else if (Input.GetButtonDown("Sprint") && (direction.magnitude >= 0.1f) && jump.isGrounded && !zoomed) //wurde links shift/rechtsklick gedrückt?
        {
                if (!sprinting) //wenn er nicht sprintet wenn man links shift/rechtsklick drückt, dann darf er sprinten
                {
                    GetUp();
                    Sprint();

                }
                else if (sprinting) //falls er sprintet und links shift/rechtsklick wieder gedrückt wird dann wird er wieder NormalSpeed haben. 
                {
                    GetUp();
                    NormalSpeed();
                }
        }
    
        //falls left alt gedrückt wird, wird der cursor angezeigt und man kann ihn frei bewegen
        if (Input.GetKey("left alt"))
        {
            Cursor.visible = true; //cursor sichtbar
            Cursor.lockState = CursorLockMode.None; //cursor ist nicht in der mitte gelocked
        }
        else
        {
            Cursor.visible = false; //cursor nicht sichtbar
            Cursor.lockState = CursorLockMode.Locked; //cursor ist in der mitte des screens gelocked
        }            

    }


    public void Walking()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        if (jump.isGrounded)
        {
            //falls der spieler am boden ist
            if (direction.magnitude >= 0.1f)
            {
                if (freeLookCam.Priority > vCam.Priority)
                {
                    //schwarze magie, versuch es erst gar nicht zu checken was quaternion oder eulerangles sind

                    targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                    angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);

                    Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                    controller.Move(moveDir.normalized * speed * Time.deltaTime);

                }
                else if (vCam.Priority > freeLookCam.Priority)
                {
                    targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                    Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                    controller.Move(moveDir * speed * Time.deltaTime);
                    
                }


                
                anim.SetBool("isWalking", true);
                if (sprinting)
                {
                    anim.SetBool("isSprinting", true);
                }
                else
                {
                    anim.SetBool("isSprinting", false);
                }

            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isSprinting", false);
                anim.SetBool("isCrouching", false);
                anim.SetBool("isCrouchwalking", false);
            }

        }
        else
        {
            if (direction.magnitude >= 0.1f)
            {

                controller.Move(transform.TransformDirection(Vector3.forward) * speed * Time.deltaTime);

            }
        }

        if (vCam.Priority > freeLookCam.Priority)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);
        }

        if (OnSlope())
        {
            controller.Move(Vector3.down * controller.height / 2 * slopeForce * Time.deltaTime);
        }

        if (crouching)
        {
            anim.SetBool("isCrouching", true);
        }
        else
        {
            anim.SetBool("isCrouching", false);
        }

    }

    //ein ienumerator für die regeneration der stamina mit parameter 
    IEnumerator StamainaReg(int stamainaRegenAmount)
    {
        //wir warten 1.5sekunden bevor unser loop ausgeführt wird
        yield return new WaitForSeconds(1.5f);

        //habe wir unter 100(maxStamina) ausdauer und wenn wir nicht sprinten, dann kann unsere stamina regenerieren
        while (currentStamaina < maxStamaina && !sprinting)
        {
            currentStamaina = Mathf.Clamp(currentStamaina, 0, maxStamaina);
            currentStamaina += stamainaRegenAmount; //stamina = stamina + 5;
            stamBar.SetStamaina(currentStamaina);
            yield return new WaitForSeconds(0.5f); //zwischen den while loops haben wir eine 1 sekündigen delay. Das heißt wir bekommen pro sekunde 5 stamina zurück wenn wir nicht sprinten
        }
    }


    IEnumerator StamainaUse(int stamainaUsed)
    {
            while (sprinting && !crouching && Time.timeScale > 0f)//wenn wir sprinten und nicht crouchen
            {
                currentStamaina -= stamainaUsed; //stamina = stamina - 5;
                stamBar.SetStamaina(currentStamaina); //damit die stamina bar unsere currentstamina ausgibt, auch während wir stamina verlieren
                currentStamaina = Mathf.Clamp(currentStamaina, 0, maxStamaina); //unsere stamina kann nur von 0 bis 100 gehen, damit wir negativ werte umgehen und auch keine werte über 100 bekommen
                yield return new WaitForSeconds(1); //das alles wird mit einem 1 sekunden delay ausgeführt zwischen den while loops

                if (currentStamaina <= 0 || vCam.Priority > freeLookCam.Priority)//wenn wir 0 stamina erreicht haben, dann gehen wir zurück zur normalen geschwindigkeit.
                {
                    NormalSpeed();
                }

            }
    }

    void FixedUpdate()
    {
        //ein raycast ist ein unsichtbarer beam der ausgeschossen wird. In diesem fall von der Mitte unsere charakters, und er wird nach oben geschossen. Mit ihm können wir mehrere sachen machen, wie in diesem beispiel eine kollision entdecken.
        Ray ray = new Ray(transform.position + offsetUp, transform.up);

        //der raycast hat eine länge von 1, falls der raycast mit etwas kollidiert, wird er rot, ansonsten, wenn er mit nichts kollidiert ist er grün.
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1, ~ignoreMe))
        {
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
            Crouch();
        }   
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 1, Color.black);
        }
    }

     bool OnSlope()
    {
        if (!jump.isGrounded)
        {
            return false;
        }

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, controller.height / 2 * slopeForceRayLength))
        {
            if (hit.normal != Vector3.up)
            {
                return true;
            }
        }
        return false;

    }

    //Respawn
    void OnTriggerEnter(Collider other)
    {
        //Sobald der Spieler mit einem Objekt kollidiert, welches ein Trigger ist, 
        //dann wird die rotation und position gleich gestellt mit der rotation und position von einem empty gameobject, welches der spawnpunkt ist
        player.transform.rotation = respawnPoint.transform.rotation;
        player.transform.position = respawnPoint.transform.position;
        charStats.currentHealth = charStats.maxHealth;
        healthBar.SetHealth(charStats.maxHealth);
        currentStamaina = maxStamaina;
        stamBar.SetStamaina(maxStamaina);
        GetUp();
        NormalSpeed();
    }

    //methode fürs crouchen
    void Crouch()
    {
        stamainaRegencrouch = StartCoroutine(StamainaReg(5));
        speed = baseSpeed - sneak;
        controller.height = reducedHeight;
        controller.center = reducedCenter;
        crouching = true;
        sprinting = false;
        StopCoroutine(stamainaRegen);
        StopCoroutine(stamainaUsing);
    }


    //methode fürs aufstehen vom crouchen
    public void GetUp()
    {
        controller.height = originalHeight;
        controller.center = originalCenter;
        crouching = false;
    }

    void Sprint()
    {
        if(currentStamaina >= 20)
        {
            speed = baseSpeed + sprint;
            sprinting = true;
            stamainaUsing = StartCoroutine(StamainaUse(5));
            StopCoroutine(stamainaRegen);
            StopCoroutine(stamainaRegencrouch);
        }
    }

    public void NormalSpeed()
    {
        speed = baseSpeed;
        sprinting = false;
        stamainaRegen = StartCoroutine(StamainaReg(5));
        StopCoroutine(stamainaUsing);
        StopCoroutine(stamainaRegencrouch);
    }   

}
