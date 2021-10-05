using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public EnemyStats eStats;
    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack() 
    {
        eStats.GetDmg(20);
    }
}
