using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour
{

    public int startYPos;
    public int endYPos;
    public int damageThreshold = 20;
    public bool damaged = false;
    public bool firstCall = true;
    public int extraDamageMultiplier = 3;
    public CharacterController controller;
    public CharacterStats charStats;
    public HealthBar healthBar;


    void Update()
    {

        if (!controller.isGrounded)
        {
            if (transform.position.y > startYPos)
            {
                firstCall = true;
            }

            if (firstCall)
            {
                startYPos = Mathf.RoundToInt(transform.position.y);
                firstCall = false;
                damaged = true;
            }
        }
        else
        {
            endYPos = Mathf.RoundToInt (transform.position.y);
            if (damaged &&(startYPos - endYPos) > damageThreshold)
            {
                damaged = false;
                firstCall = true;

                int amount = startYPos - endYPos - damageThreshold;
                int damage = (extraDamageMultiplier == 0f) ? amount : amount * extraDamageMultiplier;
                print("Fall Damage beträgt " + damage);
                charStats.currentHealth -= damage;
                healthBar.SetHealth(charStats.currentHealth);

                if(charStats.currentHealth <= 0)
                {
                    charStats.Die();
                }

            }
        }


    }
    
}
