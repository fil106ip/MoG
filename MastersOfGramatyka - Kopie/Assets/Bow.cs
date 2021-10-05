using UnityEngine;
using Cinemachine;

public class Bow : MonoBehaviour
{
    public int damage = 10;
    private float range = 100f;
    public CinemachineFreeLook freeLookCam;
    public CinemachineVirtualCamera vCam;
    public LayerMask ignoreTag;
    public Camera cam;
    public GameObject arrow;
    public Transform target;
    private float shootForce = 70f;
    public Animator bowAnim;
    private Vector3 targetPos;
    private int arrowsCount;
    //set animation to false
    private void Start()
    {
        bowAnim.SetBool("isFiring", false);
        arrowsCount = GameObject.FindGameObjectsWithTag("Arrow").Length;
    }

    void Update()
    {
        //need to be zoomed in and pressing the shoot button to shoot
        if (Input.GetMouseButtonDown(0) && vCam.Priority > freeLookCam.Priority)
        {
            Shoot();
            
            bowAnim.SetTrigger("Active");
        }
        else if (Input.GetMouseButtonUp(0) && vCam.Priority > freeLookCam.Priority)
        {
            bowAnim.SetTrigger("NotActive");
        }

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)); //a ray to the center of the screen

        if (vCam.Priority > freeLookCam.Priority)
        {

            if (Physics.Raycast(ray))
            {
                targetPos = ray.GetPoint(75);
            }
            else
            {
                targetPos = ray.GetPoint(70);
            }
            transform.LookAt(targetPos); //bow looks at center of screen, where he will also shoot
            
        }


        if (vCam.Priority > freeLookCam.Priority)
        {
            bowAnim.SetBool("isFiring", true);
        }
        else if (vCam.Priority < freeLookCam.Priority)
        {
            bowAnim.SetBool("isFiring", false);
        }
    }

    void Shoot()
    {

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)); //a ray to the center of the screen
        RaycastHit hit;

        GameObject currentArrow = Instantiate(arrow, target.transform.position, Quaternion.identity);  //cloning the arrows
        currentArrow.GetComponent<Rigidbody>().AddForce(cam.transform.forward * shootForce, ForceMode.Impulse); //force added to the arrows so they can fly

        Destroy(currentArrow, 10f);




        if (Physics.Raycast(transform.position, transform.forward, out hit, range, ~ignoreTag))
        {
            Debug.DrawLine(transform.position, hit.point);
            EnemyStats target = hit.transform.GetComponent<EnemyStats>(); //if we hit something we try to get the "EnemyStats" script
            if (target != null) //if the target exists
            {
                target.GetDmg(damage); //the target gets damage
            }
        }
 
    }

}

