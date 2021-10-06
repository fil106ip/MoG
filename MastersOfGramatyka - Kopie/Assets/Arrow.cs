using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody rigid;
    private boolean dmgAble = false;
    private boolean hasDmgd = false;
   
   private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    
    void Update() 
    {
    
        if (!hasDmgd)
        StartCoroutine(doDmg);
        else
        {
        StopCoroutine(doDmg);
        }
           
        
    }
    
    IEnumerator doDmg() 
    {
        if(dmgAble)
        {
        Shoot();
        }
        /*yield new WaitForSeconds(0.01f)
        dmgAble = false;*/
    }
    
    void Shoot()
    {
    
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)); //a ray to the center of the screen
        RaycastHit hit;
    
        if (Physics.Raycast(transform.position, transform.forward, out hit, range, ~ignoreTag))
        {
            hasDmgd = true;
            Debug.DrawLine(transform.position, hit.point);
            EnemyStats target = hit.transform.GetComponent<EnemyStats>(); //if we hit something we try to get the "EnemyStats" script
            if (target != null) //if the target exists
            {
                target.GetDmg(damage); //the target gets damage
            }
        }
    }
    

    void OnCollisionEnter(Collision Col)
     {
        dmgAble = true;
        rigid.isKinematic = true; // stop physics
        transform.parent = Col.transform; // doesn't move yet, but will move w/what it hit
     }   
}
