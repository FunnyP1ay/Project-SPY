using Cinemachine;
using Cinemachine.Utility;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

public class Playercamera : MonoBehaviour
{
    public Camera                   mainCamera; // 메인 카메라
    public Camera                   aimCamera;
    public Transform                cameraArm;
    public PlayerMove               player;
    public LayerMask                hitLayers;
    public GameObject               target_Prefab;
    public GameObject               playerGun;
    public GameObject               Aim;
    public Transform                midCamVec;
    Vector3                         targetPosition;
    public Vector3                  camRecentering;


    public bool isZoom = false;
    // Update is called once per frame
    private void Start()
    {
       StartCoroutine(Camera_MidSetting());
    }
    void Update()
    {
        if (isZoom == false)
        {
        //  LookAround();
        transform.position = player.transform.position;
            player.weaponPos.LookAt(targetPosition);

            Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

            if (Physics.Raycast(camRay, out hitInfo, 999f, hitLayers)) // , LayerMask.NameToLayer("Building")
            {
          
                // 레이가 충돌한 지점을 바라보도록 플레이어 회전
                 targetPosition = hitInfo.point;
                 targetPosition.y = player.transform.position.y; // 플레이어의 높이를 고려하여 y값 설정
               
                player.transform.LookAt(targetPosition);
                player.weaponPos.LookAt(targetPosition);

                // 드론 세팅

                if (player.weaponControl.weaponState == WeaponControl.WeaponState.skill&&hitInfo.point!=null)
                {
                    target_Prefab.SetActive(true);
                    target_Prefab.transform.position = targetPosition;
                    target_Prefab.transform.rotation = Quaternion.Euler(90f, 0f, 0f); // 회전 못하게 수정
                    player.spyAction.player_SkillAtrack.skill_targetPos = target_Prefab.transform;
                }

                else
                {
                    target_Prefab.SetActive(false);
                }
            }
        }
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Ray recenterRay = mainCamera.ScreenPointToRay(screenCenter);

        Vector3 newPosition = recenterRay.origin + recenterRay.direction * 10f; // 레이의 방향으로 위치 설정
        Aim.transform.position = newPosition;
      
  

    }
    IEnumerator Camera_MidSetting()
    {
        while (true)
        {
            midCamVec = player.gameObject.transform;
            yield return new WaitForSecondsRealtime(0.3f);
        }
    }
}
