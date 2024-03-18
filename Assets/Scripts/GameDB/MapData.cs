using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapData : MonoBehaviour
{
    private static MapData instance;
    public static MapData Instance { get { if (instance == null) return null; return instance; } }
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
    // --------------- Building Prefabs List -------------------
    [Header("BuildingPrefabs")]

    public List<Building> housePrefabs;
    public List<Building> storePrefabs;
    public List<Building> buildingPrefabs;
    public List<Building> factoryPrefabs;
    // --------------- Building Block List  ---------------------
    [Header("Block DATA")]
    public List<BuildingBlock> empty_Building_Block_List;
    public List<BuildingBlock> built_Building_Block_List;
    // ---------------      Citizen DATA    ----------------------
    [Header("Citizen DATA")]
    public int currentCitizenCount  = 0;
    public int maxCitizenCount      = 250;
    // ---------------  TrafficSignal List  ----------------------
    [Header("TrafficSignal")]
    public List<TrafficSignal> trafficSignals_List;

 
}
