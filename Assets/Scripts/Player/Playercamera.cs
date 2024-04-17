using Cinemachine;
using Cinemachine.Utility;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

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
    Vector3                         weaponPosition;

    public Vector3                  recenterRotation;
    public float                    camRecenteringCal;


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

            Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

            if (Physics.Raycast(camRay, out hitInfo, 999f, hitLayers)) // , LayerMask.NameToLayer("Building")
            {
          
                // 레이가 충돌한 지점을 바라보도록 플레이어 회전
                 targetPosition = hitInfo.point;
               
                // targetPosition.y = player.transform.position.y; // 플레이어의 높이를 고려하여 y값 설정

                //player.transform.LookAt(targetPosition);
                player.transform.forward =  (hitInfo.point - player.transform.position).normalized;

                weaponPosition = hitInfo.point;
                player.weaponPos.LookAt(weaponPosition);

                // 드론 세팅

                if (( player.weaponControl.weaponState == WeaponControl.WeaponState.skill)
                    || player.weaponControl.weaponState == WeaponControl.WeaponState.phone
                     &&  hitInfo.point!=null)
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
       
        Ray recenterRay = new Ray(player.transform.position, player.transform.forward);
        Vector3 newRecenterPosition = recenterRay.origin + recenterRay.direction * 10f; // 레이의 방향으로 위치 설정
        Aim.transform.position = newRecenterPosition;

        /*
        // ★★★현재 카메라의 회전값을 저장★★★
        Quaternion currentRotation = Aim.transform.rotation;
        // 메인 카메라의 화면 정중앙 계산
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

        // 레이의 시작 위치 
        Ray mainCamRay = mainCamera.ScreenPointToRay(screenCenter);
        // 메인카메라 정중앙으로 레이를 15f 만큼 쏨
        Vector3 mainCamCenter = mainCamRay.origin + mainCamRay.direction * 15f;
        // 원하는 방향을 향하도록 회전 각도 계산
        Vector3 direction =  mainCamCenter - newRecenterPosition;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // 부드럽게 회전시키기
        float rotationSpeed = 30f; // 회전 속도 설정
        Aim.transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
        */
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
