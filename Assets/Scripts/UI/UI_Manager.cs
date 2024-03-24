using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{

    private static UI_Manager instance;
    public static UI_Manager Instance { get { if (instance == null) return null; return instance; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(gameObject);
    }
    // ---------------- City Information ----------------
    [Header("City Information")]
    public GameObject           cityINFOPanel;
    public TextMeshProUGUI      currentCityBuildingTax;
    public TextMeshProUGUI      currentCityCitizenTax;
    public TextMeshProUGUI      currentCityBuildingCount;
    public TextMeshProUGUI      currentMayor_Approval_Rating;
    public TextMeshProUGUI      currentCitizenCount;
    public TextMeshProUGUI      currentSafety_Rating;
    [Header("Player Coat Icon")]
    public UI_Coat_Icon         ui_Player_Coat_Icon;
    [Header("Key Icon")]
    public UI_Key_Icon_Action   ui_Key_Icon_Action;
    [Header("Police Icon Panel")]
    public UI_PoliceIcon        ui_PoliceIcon;
    [Header("Law List Panel")]
    public UI_LawListPanel      ui_LawListPanel;

}
