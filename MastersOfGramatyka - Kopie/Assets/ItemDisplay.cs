using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplay : MonoBehaviour
{

    public Items item;
    void Start()
    {
        item.Print();
    }

}
