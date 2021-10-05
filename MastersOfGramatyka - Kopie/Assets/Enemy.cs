using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    /*public float lookRadius = 10f;
    private Animator anim;
    public float fightRadius = 5f;
    

    Transform target;
    NavMeshAgent agent;
    
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            print("test");
            anim.SetBool("isMoving", true);

        }
        else
        {
            anim.SetBool("isMoving", false);
        }
        

    }
 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }*/

}
