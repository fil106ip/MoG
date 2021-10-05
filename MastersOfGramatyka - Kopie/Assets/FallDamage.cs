using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour
{

    private int startYPos;
    private int endYPos;
    private readonly int damageThreshold = 20;
    private bool damaged = false;
    private bool firstCall = true;
    private readonly int extraDamageMultiplier = 5;
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
                damage = Mathf.Clamp(damage, 0, charStats.currentHealth * 30 / 100);
                print("Fall Damage beträgt " + damage);
                charStats.currentHealth -= damage;
                healthBar.SetHealth(charStats.currentHealth);

                if (charStats.currentHealth <= 0)
                {
                    charStats.Die();
                }

            }
        }


    }
    
}
