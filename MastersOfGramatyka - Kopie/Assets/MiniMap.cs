using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{

    public Transform player;
    public Camera minimap;
    public GameObject icon;
    
    void Update()
    {
        minimap.orthographicSize = Mathf.Clamp(minimap.orthographicSize + Input.GetAxis("Mouse ScrollWheel"), 25, 160);

        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            minimap.orthographicSize++;
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            minimap.orthographicSize--;
        }
    }

    void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }
}
