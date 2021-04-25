using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{

    private CharacterController controller;

    private float verticalVelocity;
    private float gravity = 23f;
    private float jumpForce = 10f;

    


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    
    void Update()
    {   //falls der spieler am boden ist
        if(controller.isGrounded)
        {   //die vertikale beschleunigung besteht aus der negativen gravitation (-19f), das heißt der spieler wird durch diese kraft nach unten gedrückt
            verticalVelocity = -gravity * Time.deltaTime;

            if (Input.GetButton("Jump"))
            {
                verticalVelocity = jumpForce;

            }
        }
        //wenn der spieler nicht am boden ist, dann wird die vertikale beschleunigung durch die gravitation schnell verringert, so dass der spieler wieder auf dem boden ist
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
    
        Vector3 moveVector = new Vector3(0, verticalVelocity, 0);
        controller.Move(moveVector * Time.deltaTime);


    }

}
