using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    //Jeder auskommentierter Code ist Feuer damage scheiße
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

    public int maxHealth = 200;
    public int currentHealth;

    public HealthBar healthBar; //script für die hp bar
    public ThirdPersonMovement thp; //script für das player movement, in dem auch respawn ist, deshalb wird der hier referenziert

    public Stat damage;
    public Stat armor;
    public Stat fireDmg;
    public bool onFire;

    
    void Awake()
    {
        currentHealth = maxHealth; //die currenthealth entspricht der maxhealth, damit man am anfang wenn man das spiel beginnt auch die max hp hat
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

        damage -= armor.GetValue(); //rüstung, wurde actually noch nich richtig ausgebaut
        damage = Mathf.Clamp(damage, 0, int.MaxValue); //der damage kann keinen negativ wert haben deswegen geht er von 0 bis int.MaxValue, das is ne zahl über 2 mio i think

        currentHealth -= damage; //wenn ich damage bekomme, soll meine hp durch den damage reduziert werden
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); //damit meine hp keine negativ werte haben und auch nich über das maxhp über gehen, begrenzen wir die hp von 0 bis maxhealth(also 200)
        Debug.Log(transform.name + " takes" + damage + " damage.");//console ausgabe

        healthBar.SetHealth(currentHealth);//wenn ich damage bekomme soll die hp bar geupdatet werden mit meiner momentanen hp

        if (currentHealth <= 0) //sobald meine hp bei 0 sind, sterbe ich
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
        player.transform.rotation = respawnPoint.transform.rotation;//wenn ich durch dmg sterbe dann soll ich am spawnpoint wieder neu spawnen
        player.transform.position = respawnPoint.transform.position;
        Debug.Log(transform.name + " died."); //console ausgabe
        currentHealth = maxHealth; //wenn ich respawne sollten meine hp wieder full sein
        healthBar.SetHealth(maxHealth);
        thp.GetUp(); //wenn ich beim ducken sterbe, dann will ich nicht mit der Hitbox vom ducken respawnen, deswegen steht der character dann wieder auf
        thp.NormalSpeed(); //wenn ich beim sprinten oder ducken sterbe, dann will ich nicht mehr am ducken oder sprinten sein wenn ich respawne
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