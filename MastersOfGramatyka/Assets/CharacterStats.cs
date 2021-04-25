using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    //Jeder auskommentierter Code ist Feuer damage scheiﬂe
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

    public int maxHealth = 200;
    public int currentHealth;

    public HealthBar healthBar;

    public Stat damage;
    public Stat armor;
    public Stat fireDmg;
    public bool onFire;

    void Awake()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(20);
            //StartCoroutine(TakeFireDmg(3, 10));
        }


    }

    public void TakeDamage(int damage)
    {

        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log(transform.name + " takes" + damage + " damage.");

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }

    }

    /*IEnumerator TakeFireDmg(int fireDmg, int damageCount)
    {
        fireDmg = Mathf.Clamp(fireDmg, 0, int.MaxValue);
        int currentCount = 0;
        onFire = true;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth(currentHealth);


        while ((currentCount < damageCount) && (onFire))
        {
            yield return new WaitForSecondsRealtime(1);
            currentHealth -= fireDmg;
            currentCount++;
            if (onFire)
            {
                damageCount++;
            }
            else
            {

            }
            healthBar.SetHealth(currentHealth);


            if (currentHealth <= 0)
            {
                Die();
                break;
            }

        }

    }*/

    public virtual void Die()
    {
        player.transform.rotation = respawnPoint.transform.rotation;
        player.transform.position = respawnPoint.transform.position;
        Debug.Log(transform.name + " died.");
        currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth);
        /*onFire = false;
        print(onFire);*/

    }

    /*void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Lava") && !onFire)
        {
            onFire = true;
            print(onFire);
            StartCoroutine(TakeFireDmg(3, 10));
        }

    }

    void OnCollisionExit(Collision col)
    {
        if(col.gameObject.CompareTag("Lava") && onFire)
        {
            onFire = false;
        }

    }*/



}