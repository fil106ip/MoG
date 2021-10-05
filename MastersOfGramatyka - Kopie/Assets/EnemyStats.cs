using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    public int maxHealth = 500;
    public int currentHealth;
    private Animator anim;

    public EnemyHealthBar ehealthBar;
    public float DeathCounter = 2;


    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        ehealthBar.SetMaxHealth(maxHealth);

    }


    public void GetDmg(int damage)
    {
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        currentHealth = Mathf.Clamp(currentHealth, 10, maxHealth);
        currentHealth -= damage;
        ehealthBar.SetHealth(currentHealth);


        if (currentHealth <= 0)
        {
            Die();
        }

    }

    public void Die()
    {
        anim.SetBool("isDead", true);
        Destroy(this.gameObject, DeathCounter);
    }


}
