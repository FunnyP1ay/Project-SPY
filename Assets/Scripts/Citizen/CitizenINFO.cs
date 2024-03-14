using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CitizenINFO : MonoBehaviour
{
    private Transform        cam;
    public  TextMeshProUGUI  nameText;
    void Start()
    {
        cam = Camera.main.transform;
    }

    //TODO : 플레이어가 특정 키를 눌렀을 때만 카메라쪽으로 보게 설정하기 (최적화 )
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            nameText.transform.LookAt(transform.position + cam.rotation * Vector3.forward, cam.rotation * Vector3.up);
        }
    }
}
