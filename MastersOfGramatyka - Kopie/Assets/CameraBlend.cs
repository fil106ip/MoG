using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraBlend : MonoBehaviour
{
    public CinemachineFreeLook freeLookCam;
    public CinemachineVirtualCamera vCam;
    public GameObject crosshair;
    public ThirdPersonMovement tpm;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            vCam.Priority = freeLookCam.Priority + 1;
            crosshair.SetActive(true);
            tpm.zoomed = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            vCam.Priority = freeLookCam.Priority - 1;
            crosshair.SetActive(false);
            tpm.zoomed = false;
        }

    }

}
