using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class AimCam : MonoBehaviour
{

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    public CinemachineVirtualCamera vcam;
    public CinemachineFreeLook freeLook;
    private Vector3 currentEulerAngles;



    void Update()
    {
        currentEulerAngles = new Vector3(pitch, yaw, 0.0f);

        if (vcam.Priority > freeLook.Priority)
        {

            Scope();
        }

   
    }

    public void Scope()
    {
        yaw += Input.GetAxis("Mouse X");
        pitch -= Input.GetAxis("Mouse Y");
        transform.localEulerAngles = currentEulerAngles;
    }
}
