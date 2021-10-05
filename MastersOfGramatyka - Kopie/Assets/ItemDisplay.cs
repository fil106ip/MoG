using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplay : MonoBehaviour
{

    public Items item;
    public GameObject player;
    readonly float radius = 5;
    void Start()
    {
        item.Print();
        player = GameObject.FindWithTag("Player");
    }

     void Update()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        //schalter
        if (dist < radius)
        {
            if (Input.GetButtonDown("Interaction"))
            {
                print("you can pick up the item");
            }
        }
    }



}
