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

    public IEnumerator zoomSPYAction()
    {
        iszoomSPYAction = true;
        playercamera.isZoom = true;
        zoomVirtualCamera.Priority = 20;
        yield return new WaitForSecondsRealtime(2f);
        iszoomSPYAction = false;
        playercamera.isZoom = false;
        zoomVirtualCamera.Priority = 5;
    }
    //public IEnumerator 
}
