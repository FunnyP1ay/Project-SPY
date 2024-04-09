using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Steamworks.InventoryItem;

public class INGame_UI_DATASetting : MonoBehaviour
{
    [Header("City Information")]
    public GameObject cityINFOPanel;
    public TextMeshProUGUI currentCityBuildingTax;
    public TextMeshProUGUI currentCityCitizenTax;
    public TextMeshProUGUI currentCityBuildingCount;
    public TextMeshProUGUI currentMayor_Approval_Rating;
    public TextMeshProUGUI currentCitizenCount;
    public TextMeshProUGUI currentSafety_Rating;
    [Header("Close Panel")]
    public GameObject       ui_ClosePanel;
    [Header("Die Penal")]
    public GameObject       ui_DiePenal;
    [Header("MissionClear Panel")]
    public GameObject       ui_MissionClearPanel;
    public TextMeshProUGUI  missionClearText;
    [Header("Buttons")]
    public Button mainMenuButton;
    public Button returnGameButton;

    void Start()
    {
        cityINFOPanel = transform.Find("CITY_INFO_Panel").gameObject;


        /*
        currentCityBuildingTax = transform.Find("CurrentBuildingTax").GetComponent<TextMeshProUGUI>();
        currentCityCitizenTax = transform.Find("CurrentCityzenTax").GetComponent<TextMeshProUGUI>();
        currentCityBuildingCount = transform.Find("CurrentBuildingCount").GetComponent<TextMeshProUGUI>();
        currentCitizenCount = transform.Find("CurrentCityzenCount").GetComponent<TextMeshProUGUI>();
        currentMayor_Approval_Rating = transform.Find("Mayor_Approval_Rating").GetComponent<TextMeshProUGUI>();
        currentSafety_Rating = transform.Find("CurrentSafetyRating").GetComponent<TextMeshProUGUI>();
                ui_ClosePanel = transform.Find("ClosePanel").gameObject;
        ui_DiePenal = transform.Find("DiePanel").gameObject;
        */

        UI_Manager.Instance.cityINFOPanel = this.cityINFOPanel;
        UI_Manager.Instance.currentCityBuildingTax = this.currentCityBuildingTax;
        UI_Manager.Instance.currentCityCitizenTax = this.currentCityCitizenTax;
        UI_Manager.Instance.currentCityBuildingCount = this.currentCityBuildingCount;
        UI_Manager.Instance.currentMayor_Approval_Rating = this.currentMayor_Approval_Rating;
        UI_Manager.Instance.currentCitizenCount = this.currentCitizenCount;
        UI_Manager.Instance.currentSafety_Rating = this.currentSafety_Rating;

        UI_Manager.Instance.ui_ClosePanel = this.ui_ClosePanel;
        UI_Manager.Instance.ui_DiePenal = this.ui_DiePenal;
        
        QuestManager.Instance.missionClearPanel = this.ui_MissionClearPanel.gameObject;
        QuestManager.Instance.missionClearText = this.missionClearText;

        SceneLoader.Instance.mainMenuButton = this.mainMenuButton;
        SceneLoader.Instance.returnGameButton = this.returnGameButton;
        SceneLoader.Instance.InGameButtonSetting();
        this.ui_ClosePanel.gameObject.SetActive(false);
        this.ui_DiePenal.gameObject.SetActive(false);
        this.ui_MissionClearPanel.gameObject.SetActive(false);
        this.cityINFOPanel.gameObject.SetActive(false);


    }
}
