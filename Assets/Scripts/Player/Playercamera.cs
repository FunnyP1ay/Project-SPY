using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercamera : MonoBehaviour
{
    public Camera mainCamera; // 메인 카메라
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

        // 테스트 코드
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 99999f, LayerMask.NameToLayer("Building")))
            {
                // 레이가 충돌한 지점을 바라보도록 플레이어 회전
                Vector3 targetPosition = hitInfo.point;
                targetPosition.y = PlayerBody.position.y; // 플레이어의 높이를 고려하여 y값 설정
                PlayerBody.LookAt(targetPosition);
            
            }
        }
 
    }
   
}
