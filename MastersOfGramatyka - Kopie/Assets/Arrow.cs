using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision Col)
     {
        rigid.isKinematic = true; // stop physics
        transform.parent = Col.transform; // doesn't move yet, but will move w/what it hit
     }   
}
