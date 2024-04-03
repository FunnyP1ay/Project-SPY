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
                GoodLawSetting();
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
                if (CityControlData.Instance.martial_Law)
                {
                    
                    CityControlData.Instance.SetApprovalRating(-3f);
                    UI_Manager.Instance.currentLawIcon_List.Add(UI_Manager.Instance.ui_LawListPanel.martial_Law);
                    UI_Manager.Instance.ui_News.AddNews(UI_Manager.Instance.ui_News.LawerNew_1);
                }
                else
                {
                    print("정부가 계엄령을 해제 했습니다.");
                    UI_Manager.Instance.currentLawIcon_List.Remove(UI_Manager.Instance.ui_LawListPanel.martial_Law);
                    UI_Manager.Instance.ui_News.AddNews(UI_Manager.Instance.ui_News.LawerNew_2);
                }
                break;

                case 1:
                    CityControlData.Instance.travel_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.travel_Law);
                if (CityControlData.Instance.travel_Law)
                {
                    print("정부가 여행 금지를 선포 했습니다.");
                    CityControlData.Instance.SetApprovalRating(-0.2f);
                    UI_Manager.Instance.currentLawIcon_List.Add(UI_Manager.Instance.ui_LawListPanel.travel_Law);
                    UI_Manager.Instance.ui_News.AddNews(UI_Manager.Instance.ui_News.LawerNew_3);
                }
                else
                {
                    CityControlData.Instance.SetApprovalRating(0.2f);
                    UI_Manager.Instance.currentLawIcon_List.Remove(UI_Manager.Instance.ui_LawListPanel.travel_Law);
                    UI_Manager.Instance.ui_News.AddNews(UI_Manager.Instance.ui_News.LawerNew_4);
                    print("정부가 여행 허용을 선포 했습니다.");
                }
                break;

                case 2:
                CityControlData.Instance.romance_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.romance_Law);
                if (CityControlData.Instance.romance_Law)
                {
                    print("정부가 연애 금지를 선포 했습니다.");
                    CityControlData.Instance.SetApprovalRating(-0.5f);
                    UI_Manager.Instance.currentLawIcon_List.Add(UI_Manager.Instance.ui_LawListPanel.romance_Law);
                    UI_Manager.Instance.ui_News.AddNews(UI_Manager.Instance.ui_News.LawerNew_5);
                }
                else
                {
                    CityControlData.Instance.SetApprovalRating(0.3f);
                    UI_Manager.Instance.currentLawIcon_List.Remove(UI_Manager.Instance.ui_LawListPanel.romance_Law);
                    UI_Manager.Instance.ui_News.AddNews(UI_Manager.Instance.ui_News.LawerNew_6);
                }
                break;

                case 3:
                CityControlData.Instance.game_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.game_Law);
                if (CityControlData.Instance.game_Law)
                {
                    print("정부가 게임 금지를 선포 했습니다.");
                    CityControlData.Instance.SetApprovalRating(-1f);
                    UI_Manager.Instance.currentLawIcon_List.Add(UI_Manager.Instance.ui_LawListPanel.game_Law);
                    UI_Manager.Instance.ui_News.AddNews(UI_Manager.Instance.ui_News.LawerNew_7);
                }
                else
                {
                    CityControlData.Instance.SetApprovalRating(1f);
                    UI_Manager.Instance.currentLawIcon_List.Remove(UI_Manager.Instance.ui_LawListPanel.game_Law);
                    UI_Manager.Instance.ui_News.AddNews(UI_Manager.Instance.ui_News.LawerNew_8);
                    print("정부가 게임 허용을 선포 했습니다.");
                }
                break;

                case 4:
                CityControlData.Instance.prohibition_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.prohibition_Law);
                if (CityControlData.Instance.prohibition_Law)
                {
                    print("정부가 금주령을 선포 했습니다.");
                    CityControlData.Instance.SetApprovalRating(-0.4f);
                    UI_Manager.Instance.currentLawIcon_List.Add(UI_Manager.Instance.ui_LawListPanel.prohibition_Law);
                    UI_Manager.Instance.ui_News.AddNews(UI_Manager.Instance.ui_News.LawerNew_9);
                }
                else
                {
                    CityControlData.Instance.SetApprovalRating(0.2f);
                    UI_Manager.Instance.currentLawIcon_List.Remove(UI_Manager.Instance.ui_LawListPanel.prohibition_Law);
                    UI_Manager.Instance.ui_News.AddNews(UI_Manager.Instance.ui_News.LawerNew_10);
                    print("정부가 금주령을 해제 했습니다.");
                }
                break;

                case 5:
                CityControlData.Instance.cigarette_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.cigarette_Law);
                if (CityControlData.Instance.cigarette_Law)
                {
                    print("정부가 흡연 금지를 선포 했습니다.");
                    CityControlData.Instance.SetApprovalRating(-0.5f);
                    UI_Manager.Instance.currentLawIcon_List.Add(UI_Manager.Instance.ui_LawListPanel.cigarette_Law);
                    UI_Manager.Instance.ui_News.AddNews(UI_Manager.Instance.ui_News.LawerNew_11);
                }
                else
                {
                    CityControlData.Instance.SetApprovalRating(0.3f);
                    UI_Manager.Instance.currentLawIcon_List.Remove(UI_Manager.Instance.ui_LawListPanel.cigarette_Law);
                    UI_Manager.Instance.ui_News.AddNews(UI_Manager.Instance.ui_News.LawerNew_12);
                    print("정부가 흡연 허용을 선포 했습니다.");
                }
                break;

                case 6:
                CityControlData.Instance.pets_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.pets_Law);
                if (CityControlData.Instance.pets_Law)
                {
                    print("정부가 반려동물 금지를 선포 했습니다.");
                    CityControlData.Instance.SetApprovalRating(-0.5f);
                    UI_Manager.Instance.currentLawIcon_List.Add(UI_Manager.Instance.ui_LawListPanel.pets_Law);
                    UI_Manager.Instance.ui_News.AddNews(UI_Manager.Instance.ui_News.LawerNew_13);
                }
                else
                {
                    CityControlData.Instance.SetApprovalRating(0.4f);
                    UI_Manager.Instance.currentLawIcon_List.Remove(UI_Manager.Instance.ui_LawListPanel.pets_Law);
                    UI_Manager.Instance.ui_News.AddNews(UI_Manager.Instance.ui_News.LawerNew_14);
                    print("정부가 반려동물 허용을 선포 했습니다.");
                }
                break;

                case 7:
                CityControlData.Instance.tax_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.tax_Law);
                if (CityControlData.Instance.tax_Law)
                {
                    print("정부가 세금 인상을 선포 했습니다.");
                    CityControlData.Instance.SetApprovalRating(-1f);
                    UI_Manager.Instance.currentLawIcon_List.Add(UI_Manager.Instance.ui_LawListPanel.tax_Law);
                    UI_Manager.Instance.ui_News.AddNews(UI_Manager.Instance.ui_News.LawerNew_15);
                }
                else
                {
                    CityControlData.Instance.SetApprovalRating(1f);
                    UI_Manager.Instance.currentLawIcon_List.Remove(UI_Manager.Instance.ui_LawListPanel.tax_Law);
                    UI_Manager.Instance.ui_News.AddNews(UI_Manager.Instance.ui_News.LawerNew_16);
                    print("정부가 세금 인하을 선포 했습니다.");
                }
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
                if (CityControlData.Instance.sportEvent_Law)
                {
                    print("정부가 스포츠 이벤트를 지원하기로 했습니다.");
                    CityControlData.Instance.SetApprovalRating(0.1f);
                    UI_Manager.Instance.currentLawIcon_List.Add(UI_Manager.Instance.ui_LawListPanel.sportEvent_Law);
                    UI_Manager.Instance.ui_News.AddNews(UI_Manager.Instance.ui_News.LawerNew_17);
                }
                else
                {
                    CityControlData.Instance.SetApprovalRating(-0.1f);
                    print("정부가 스포츠 이벤트를 중단하기로 했습니다.");
                    UI_Manager.Instance.currentLawIcon_List.Remove(UI_Manager.Instance.ui_LawListPanel.sportEvent_Law);
                    UI_Manager.Instance.ui_News.AddNews(UI_Manager.Instance.ui_News.LawerNew_18);
                }
                break; 
            case 1:
                CityControlData.Instance.freeHair_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.freeHair_Law);
                if (CityControlData.Instance.freeHair_Law)
                {
                    print("정부가 두발자유 허용를 선포 했습니다.");
                    CityControlData.Instance.SetApprovalRating(0.1f);
                    UI_Manager.Instance.currentLawIcon_List.Add(UI_Manager.Instance.ui_LawListPanel.freeHair_Law);
                    UI_Manager.Instance.ui_News.AddNews(UI_Manager.Instance.ui_News.LawerNew_19);
                }
                else
                {
                    CityControlData.Instance.SetApprovalRating(-1f);
                    UI_Manager.Instance.currentLawIcon_List.Remove(UI_Manager.Instance.ui_LawListPanel.freeHair_Law);
                    UI_Manager.Instance.ui_News.AddNews(UI_Manager.Instance.ui_News.LawerNew_20);
                    print("정부가 단발령을 선포 했습니다.");
                }
                break;
            case 2:
                CityControlData.Instance.gym_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.gym_Law);
                if (CityControlData.Instance.gym_Law)
                {
                    print("정부가 헬스장 출입 금지를 선포 했습니다.");
                    CityControlData.Instance.SetApprovalRating(-1f);
                    UI_Manager.Instance.currentLawIcon_List.Add(UI_Manager.Instance.ui_LawListPanel.gym_Law);
                    UI_Manager.Instance.ui_News.AddNews(UI_Manager.Instance.ui_News.LawerNew_21);
                }
                else
                {
                    CityControlData.Instance.SetApprovalRating(1f);
                    UI_Manager.Instance.currentLawIcon_List.Remove(UI_Manager.Instance.ui_LawListPanel.gym_Law);
                    UI_Manager.Instance.ui_News.AddNews(UI_Manager.Instance.ui_News.LawerNew_22);
                    print("정부가 헬스장 금지를 해제 했습니다.");
                }
                break;
            case 3:
                CityControlData.Instance.parade_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.parade_Law);
                if (CityControlData.Instance.parade_Law)
                {
                    print("정부가 퍼레이드 지원을 선포 했습니다.");
                    CityControlData.Instance.SetApprovalRating(0.1f);
                    UI_Manager.Instance.currentLawIcon_List.Add(UI_Manager.Instance.ui_LawListPanel.parade_Law);
                    UI_Manager.Instance.ui_News.AddNews(UI_Manager.Instance.ui_News.LawerNew_23);
                }
                else
                {
                    CityControlData.Instance.SetApprovalRating(-0.1f);
                    UI_Manager.Instance.currentLawIcon_List.Remove(UI_Manager.Instance.ui_LawListPanel.parade_Law);
                    UI_Manager.Instance.ui_News.AddNews(UI_Manager.Instance.ui_News.LawerNew_24);
                    print("정부가 퍼레이드 금지를 선포 했습니다.");
                }
                break;
            case 4:
                CityControlData.Instance.mayorsMovie_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.mayorsMovie_Law);
                if (CityControlData.Instance.mayorsMovie_Law)
                {
                    print("정부가 위대한 우리 시장님 영화를 개봉 했습니다.");
                    CityControlData.Instance.SetApprovalRating(3f);
                    UI_Manager.Instance.currentLawIcon_List.Add(UI_Manager.Instance.ui_LawListPanel.mayorsMovie_Law);
                    UI_Manager.Instance.ui_News.AddNews(UI_Manager.Instance.ui_News.LawerNew_25);
                }
                else
                {
                    CityControlData.Instance.SetApprovalRating(1f);
                    UI_Manager.Instance.currentLawIcon_List.Remove(UI_Manager.Instance.ui_LawListPanel.mayorsMovie_Law);
                }
                break;
            case 5:
                CityControlData.Instance.hospital_Law = CityControlData.Instance.LawStateChange(CityControlData.Instance.hospital_Law);
                if (CityControlData.Instance.hospital_Law)
                {
                    print("정부가 병원비 지원을 선포 했습니다.");
                    CityControlData.Instance.SetApprovalRating(0.3f);
                    UI_Manager.Instance.currentLawIcon_List.Add(UI_Manager.Instance.ui_LawListPanel.hospital_Law);
                    UI_Manager.Instance.ui_News.AddNews(UI_Manager.Instance.ui_News.LawerNew_26);
                }
                else
                {
                    CityControlData.Instance.SetApprovalRating(-0.3f);
                    UI_Manager.Instance.currentLawIcon_List.Remove(UI_Manager.Instance.ui_LawListPanel.hospital_Law);
                    UI_Manager.Instance.ui_News.AddNews(UI_Manager.Instance.ui_News.LawerNew_27);
                    print("정부가 병원비 지원을 중단 했습니다.");
                }
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
            default: 
                break;
        }
    }
}
