using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Cinemachine_Control : MonoBehaviour
{
    public CinemachineVirtualCamera     zoomVirtualCamera;
    public Playercamera                 playercamera;
    private CinemachineImpulseSource    impulseSource;
    public bool                         iszoomSPYAction = false;
    public bool                         isDroneSPYAction = false;

    private void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Fire_Impulse()
    {
        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse();
        }
    }
    public void ZoomSPYActionStart()
    {
        iszoomSPYAction = true;
        playercamera.isZoom = true;
        zoomVirtualCamera.Priority = 20;
    }


    public void ZoomSPYActionFinsh() 
    { 
        iszoomSPYAction = false;
        playercamera.isZoom = false;
        zoomVirtualCamera.Priority = 5;
    }

    public void BroneSPYActionStart()
    {
        isDroneSPYAction = true;
        playercamera.isZoom = true;
    }
    public void BroneSPYActionFinsh()
    {
        isDroneSPYAction = false;
        playercamera.isZoom = false;
    }
    //public IEnumerator 
}
