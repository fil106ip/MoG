using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            Camera.main.fieldOfView++;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            Camera.main.fieldOfView--;
        }
    }
}
