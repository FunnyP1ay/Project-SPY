using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Playercamera : MonoBehaviour
{
    public Camera       mainCamera; // ���� ī�޶�
    public Transform    cameraArm;
    public PlayerMove   player;
    public LayerMask    hitLayers;
    public GameObject   target_Prefab;
    public GameObject   playerGun;
    

    public bool isZoom = false;
    // Update is called once per frame
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
                Vector3 targetPosition = hitInfo.point;
                targetPosition.y = player.transform.position.y; // �÷��̾��� ���̸� ����Ͽ� y�� ����

                player.transform.LookAt(targetPosition);
                player.weaponPos.LookAt(targetPosition);
                // ��� ����

                if (player.weaponControl.weaponState == WeaponControl.WeaponState.skill&&hitInfo.point!=null)
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
 
    }
   
}
