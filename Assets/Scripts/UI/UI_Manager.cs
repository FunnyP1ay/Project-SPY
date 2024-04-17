using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{

    private static UI_Manager instance;
    public static UI_Manager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(this.gameObject);
        }
        DOTween.Init(); // DOTween √ ±‚»≠
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
    [Header("Event Camera Panel")]
    public UI_EventCarmera      eventCarmera;
    [Header("Close Panel")]
    public GameObject           ui_ClosePanel;
    [Header("Law List Panel")]
    public UI_LawListPanel      ui_LawListPanel;
    public List<Sprite>         currentLawIcon_List = new List<Sprite>();
    [Header("News Panel")]
    public UI_News              ui_News;
    [Header("Die Penal")]
    public GameObject           ui_DiePenal;
    [Header("Drone View Panel")]
    public GameObject           droneViewPanel;



    public void ResetDATA()
    {
        currentLawIcon_List.Clear();
    }
    public void PopUp(GameObject _panel, bool _value)
    {
        if (_value == false)
            ShowPopUp(_panel);
        else
            HidePopUp(_panel);
    }
    private void ShowPopUp(GameObject _panel)
    {
        
        _panel.gameObject.SetActive(true);
        _panel.transform.localScale = Vector3.zero;
        _panel.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
    } 
    private void HidePopUp(GameObject _panel)
    {
        _panel.transform.DOScale(0f, 0.3f).SetEase(Ease.InBack);
       
    }

    
}
