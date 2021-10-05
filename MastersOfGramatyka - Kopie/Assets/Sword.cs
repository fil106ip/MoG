using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private int damage = 10;
    public EnemyStats target;
    
    

    private void OnCollisionEnter(Collision col)
    {
       if (col.gameObject.CompareTag("Enemy"))
       {
            print("enemy");
            if (Input.GetMouseButton(0))
            {
           
                target.GetDmg(damage);
            }
       }
    }
}
