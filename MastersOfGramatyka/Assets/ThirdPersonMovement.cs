using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;
 
    public CharacterController controller;
    public Transform cam;
    public Vector3 _prevPosition;

    public float speed = 6f;
    public float sprint = 6f;
    public float baseSpeed = 6f;
    public float sneak = 3f;
    public float rollDistance = 10f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Vector3 originalCenter;
    public Vector3 reducedCenter; 

    public float originalHeight;
    public float reducedHeight;
    bool sprinting; 
    bool crouching;
    bool sneaking;
    bool rolling;

    public CharacterStats charStats;
    public HealthBar healthBar;

    //am anfang wird festgelegt, dass die originalhöhe der controller höhe entspricht
    void Start()
    {
        originalHeight = controller.height;
    }

    void Update()
    {

        // ein vektor wird kreiert, aber nur die x und z achse wird geändert, die y achse bleibt immer 0f
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical);
       

        //falls der spieler am boden ist
        if (direction.magnitude >= 0.1f)
        {
            //irgendwas mit cam und rotation keine ahnung man wer hat sich diese scheiße ausgedacht warum wurde ich net gärtner uff
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            
        }


            if (Input.GetKeyDown("left ctrl"))
            {
                if (!crouching)
                {
                    Crouch();
                    Sneak();
                }
                else
                {
                    GetUp();
                    normalSpeed();
                }


            }
            else if (Input.GetKeyDown("left shift"))
            {
                if (!sprinting)
                {
                    GetUp();
                    Sprint();
                }
                else
                {
                    GetUp();
                    normalSpeed();
                }
            }



        //falls left alt gedrückt wird, wird der cursor angezeigt und man kann ihn frei bewegen
        if (Input.GetKey("left alt"))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        } 

       
    }

    void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, Vector3.up);
        RaycastHit hitInfo;

        //der raycast hat eine länge von 5, falls der raycast mit etwas kollidiert, wird er rot, ansonsten, wenn er mit nichts kollidiert ist er grün.
        if (Physics.Raycast(ray, out hitInfo, 1))
        {
            //print(hitInfo.collider.gameObject.name);
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
            Crouch();
            Sneak();
        }   
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 1, Color.green);
        }

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

        GetUp();
        normalSpeed();
    } 

    //methode fürs crouchen
    void Crouch()
    {
        controller.height = reducedHeight;
        controller.center = reducedCenter;
        crouching = true;
    }

    //methode fürs aufstehen vom crouchen
    void GetUp()
    {
        controller.height = originalHeight;
        controller.center = originalCenter;
        crouching = false;
    }

    void Sprint()
    {
        speed = baseSpeed + sprint;
        sprinting = true; 
        
    }

    void Sneak()
    {
        speed = baseSpeed - sneak;
        sneaking = true;
    }

    void normalSpeed()
    {
        speed = baseSpeed;
        sneaking = false;
        sprinting = false; 
    }

}
