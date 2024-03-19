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
                print("정부가 계엄령을 선포 했습니다.");
                break;

                case 1:
                    CityControlData.Instance.travel_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.travel_Law);
                if(CityControlData.Instance.travel_Law)
                    print("정부가 여행 금지를 선포 했습니다.");
                else
                    print("정부가 여행 허용을 선포 했습니다.");
                break;

                case 2:
                CityControlData.Instance.romance_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.romance_Law);
                print("정부가 연애 금지를 선포 했습니다.");
                break;

                case 3:
                CityControlData.Instance.game_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.game_Law);
                print("정부가 게임 금지를 선포 했습니다.");
                break;

                case 4:
                CityControlData.Instance.prohibition_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.prohibition_Law);
                print("정부가  금주령을 선포 했습니다.");
                break;

                case 5:
                CityControlData.Instance.cigarette_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.cigarette_Law);
                print("정부가 흡연 금지를 선포 했습니다.");
                break;

                case 6:
                CityControlData.Instance.pets_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.pets_Law);
                print("정부가 반려동물 금지를 선포 했습니다.");
                break;

                case 7:
                CityControlData.Instance.tax_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.tax_Law);
                print("정부가 세금 인상을 선포 했습니다.");
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
                print("정부가 스포츠 이벤트를 지원하기로 했습니다.");
                break; 
            case 1:
                CityControlData.Instance.freeHair_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.freeHair_Law);
                print("정부가 두발자유 허용를 선포 했습니다.");
                break;
            case 2:
                CityControlData.Instance.cannabis_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.cannabis_Law);
                print("정부가 대마초 허용를 선포 했습니다.");
                break;
            case 3:
                CityControlData.Instance.parade_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.parade_Law);
                print("정부가 퍼레이드 지원을 선포 했습니다.");
                break;
            case 4:
                CityControlData.Instance.mayorsMovie_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.mayorsMovie_Law);
                print("정부가 위대한 우리 시장님 영화를 개봉 했습니다.");
                break;
            case 5:
                CityControlData.Instance.hospital_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.hospital_Law);
                print("정부가 병원비 지원을 선포 했습니다.");
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
