using System.Collections;
using System.Collections.Generic;
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

    [Header("Block DATA")]
    // --------------- Building Block List ---------------------
    public List<BuildingBlock> empty_Building_Block_List;
    public List<BuildingBlock> built_Building_Block_List;
    // --------------- TrafficSignal List ----------------------
    [Header("TrafficSignal")]
    public List<TrafficSignal> trafficSignals_List;

}
