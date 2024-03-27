using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercamera : MonoBehaviour
{
    public Camera mainCamera; // ���� ī�޶�
    public Transform cameraArm;
    public PlayerMove player;

    public bool isZoom = false;
    // Update is called once per frame
    void Update()
    {
        if (isZoom == false)
        {
        //  LookAround();
        transform.position = player.transform.position;

        // �׽�Ʈ �ڵ�
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 99999f, LayerMask.NameToLayer("Building")))
            {
                // ���̰� �浹�� ������ �ٶ󺸵��� �÷��̾� ȸ��
                Vector3 targetPosition = hitInfo.point;
                targetPosition.y = player.transform.position.y; // �÷��̾��� ���̸� ����Ͽ� y�� ����
                player.transform.LookAt(targetPosition);
                player.weaponPos.LookAt(targetPosition);
            }
        }
 
    }
   
}
