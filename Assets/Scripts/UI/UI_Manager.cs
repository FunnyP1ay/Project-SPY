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
    [Header("City Laws Informtion")]
    public List<Image>          currentlawList;
}
