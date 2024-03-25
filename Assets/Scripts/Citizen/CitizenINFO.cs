using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CitizenINFO : MonoBehaviour
{
    private Transform        cam;
    public  TextMeshProUGUI  nameText;
    public  int              citizenMoney;

    private void Start()
    {
        cam = Camera.main.transform;
    }

         //nameText.transform.LookAt(transform.position + cam.rotation * Vector3.forward, cam.rotation * Vector3.up);
    public void GetMoney(int _Value)
    {
        citizenMoney += _Value;
    }
    public void TakeMoney(int _Cost)
    {
        if (citizenMoney >= _Cost)
        {

            citizenMoney -= _Cost;
            CityControlData.Instance.citizen_Tax += _Cost;
            print("�ù��� ������ �½��ϴ� !");
        }
        else
            print("�ù��� ���� �����մϴ�."); //TODO �� ���°� ��Ȳ ,����
    }
}
