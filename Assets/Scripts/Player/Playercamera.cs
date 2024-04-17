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
    public Camera                   mainCamera; // ���� ī�޶�
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
          
                // ���̰� �浹�� ������ �ٶ󺸵��� �÷��̾� ȸ��
                 targetPosition = hitInfo.point;
               
                // targetPosition.y = player.transform.position.y; // �÷��̾��� ���̸� ����Ͽ� y�� ����

                //player.transform.LookAt(targetPosition);
                player.transform.forward =  (hitInfo.point - player.transform.position).normalized;

                weaponPosition = hitInfo.point;
                player.weaponPos.LookAt(weaponPosition);

                // ��� ����

                if (( player.weaponControl.weaponState == WeaponControl.WeaponState.skill)
                    || player.weaponControl.weaponState == WeaponControl.WeaponState.phone
                     &&  hitInfo.point!=null)
                {
                    target_Prefab.SetActive(true);
                    target_Prefab.transform.position = targetPosition;
                    target_Prefab.transform.rotation = Quaternion.Euler(90f, 0f, 0f); // ȸ�� ���ϰ� ����
                    player.spyAction.player_SkillAtrack.skill_targetPos = target_Prefab.transform;
                }
                else
                {
                    target_Prefab.SetActive(false);
                }
            }
        }
       
        Ray recenterRay = new Ray(player.transform.position, player.transform.forward);
        Vector3 newRecenterPosition = recenterRay.origin + recenterRay.direction * 10f; // ������ �������� ��ġ ����
        Aim.transform.position = newRecenterPosition;

        /*
        // �ڡڡ����� ī�޶��� ȸ������ ����ڡڡ�
        Quaternion currentRotation = Aim.transform.rotation;
        // ���� ī�޶��� ȭ�� ���߾� ���
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

        // ������ ���� ��ġ 
        Ray mainCamRay = mainCamera.ScreenPointToRay(screenCenter);
        // ����ī�޶� ���߾����� ���̸� 15f ��ŭ ��
        Vector3 mainCamCenter = mainCamRay.origin + mainCamRay.direction * 15f;
        // ���ϴ� ������ ���ϵ��� ȸ�� ���� ���
        Vector3 direction =  mainCamCenter - newRecenterPosition;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // �ε巴�� ȸ����Ű��
        float rotationSpeed = 30f; // ȸ�� �ӵ� ����
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
