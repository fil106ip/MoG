using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Items : ScriptableObject
{
    public new string name;
    public string description;

    public int health;
    public int cost;
    public int attack;
    public int healthRegen;

    public void Print()
    {
        Debug.Log(name + ": " + description);
    }

}
