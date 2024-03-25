using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercamera : MonoBehaviour
{
    public Camera mainCamera; // ���� ī�޶�
    public Transform cameraArm;
    public Transform PlayerBody;

    public bool isZoom = false;
    // Update is called once per frame
    void Update()
    {
        if (isZoom == false)
        {
        //  LookAround();
        transform.position = PlayerBody.transform.position;

        // �׽�Ʈ �ڵ�
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 99999f, LayerMask.NameToLayer("Building")))
            {
                // ���̰� �浹�� ������ �ٶ󺸵��� �÷��̾� ȸ��
                Vector3 targetPosition = hitInfo.point;
                targetPosition.y = PlayerBody.position.y; // �÷��̾��� ���̸� ����Ͽ� y�� ����
                PlayerBody.LookAt(targetPosition);
            
            }
        }
 
    }
   
}
