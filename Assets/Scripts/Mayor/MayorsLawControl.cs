using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MayorsLawControl : MonoBehaviour
{
    private int randNum;
    private int value;
    public void CheckMayor_LawOrder()
    {
        value = Random.Range(0, 4);
        switch (value)
        {
            case 0:
                BadLawSetting();
                break;
            case 1:
                GoodLawSetting();
                break;
            case 2:
                GoodLawSetting();
                break;
            default: 
                break;
        }
    }
    public void BadLawSetting()
    {
        randNum = Random.Range(0, 8);
        switch (randNum)
        {    
                case 0:
                CityControlData.Instance.martial_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.martial_Law);
                print("���ΰ� ������� ���� �߽��ϴ�.");
                break;

                case 1:
                    CityControlData.Instance.travel_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.travel_Law);
                if(CityControlData.Instance.travel_Law)
                    print("���ΰ� ���� ������ ���� �߽��ϴ�.");
                else
                    print("���ΰ� ���� ����� ���� �߽��ϴ�.");
                break;

                case 2:
                CityControlData.Instance.romance_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.romance_Law);
                print("���ΰ� ���� ������ ���� �߽��ϴ�.");
                break;

                case 3:
                CityControlData.Instance.game_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.game_Law);
                print("���ΰ� ���� ������ ���� �߽��ϴ�.");
                break;

                case 4:
                CityControlData.Instance.prohibition_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.prohibition_Law);
                print("���ΰ�  ���ַ��� ���� �߽��ϴ�.");
                break;

                case 5:
                CityControlData.Instance.cigarette_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.cigarette_Law);
                print("���ΰ� �� ������ ���� �߽��ϴ�.");
                break;

                case 6:
                CityControlData.Instance.pets_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.pets_Law);
                print("���ΰ� �ݷ����� ������ ���� �߽��ϴ�.");
                break;

                case 7:
                CityControlData.Instance.tax_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.tax_Law);
                print("���ΰ� ���� �λ��� ���� �߽��ϴ�.");
                break;  

                default:
                break;
        }
    }
    public void GoodLawSetting()
    {

        randNum = Random.Range(0, 6);

        switch (randNum)
        {
            case 0:
                CityControlData.Instance.sportEvent_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.sportEvent_Law);
                print("���ΰ� ������ �̺�Ʈ�� �����ϱ�� �߽��ϴ�.");
                break; 
            case 1:
                CityControlData.Instance.freeHair_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.freeHair_Law);
                print("���ΰ� �ι����� ��븦 ���� �߽��ϴ�.");
                break;
            case 2:
                CityControlData.Instance.cannabis_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.cannabis_Law);
                print("���ΰ� �븶�� ��븦 ���� �߽��ϴ�.");
                break;
            case 3:
                CityControlData.Instance.parade_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.parade_Law);
                print("���ΰ� �۷��̵� ������ ���� �߽��ϴ�.");
                break;
            case 4:
                CityControlData.Instance.mayorsMovie_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.mayorsMovie_Law);
                print("���ΰ� ������ �츮 ����� ��ȭ�� ���� �߽��ϴ�.");
                break;
            case 5:
                CityControlData.Instance.hospital_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.hospital_Law);
                print("���ΰ� ������ ������ ���� �߽��ϴ�.");
                break;
            default : 
                break;


        }
    }

    public void ComicLawSetting()
    {
        randNum = Random.Range(0, 9);
        /* 
      public bool marshmallow_Law;
      public bool sleep_Law;
      public bool chocolate_Law;
      public bool pizza_Law;
      public bool goodMorning_Law;
      public bool rockScissorsPaper_Law;
      public bool cola_Law;
      public bool handstand_Law;
      public bool brushingTeeth_Law;*/
        switch (randNum)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            default: 
                break;
        }
    }
}
