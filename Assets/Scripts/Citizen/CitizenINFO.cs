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

    //TODO : �÷��̾ Ư�� Ű�� ������ ���� ī�޶������� ���� �����ϱ� (����ȭ )
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            nameText.transform.LookAt(transform.position + cam.rotation * Vector3.forward, cam.rotation * Vector3.up);
        }
    }
}
