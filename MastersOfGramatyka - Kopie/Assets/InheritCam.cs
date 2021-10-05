using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InheritCam : MonoBehaviour
{
    public GameObject vCam;
    public GameObject fCam;

    // Update is called once per frame
    void Update()
    {
        vCam.transform.rotation = fCam.transform.rotation;
        vCam.transform.position = fCam.transform.position;
    }
}
