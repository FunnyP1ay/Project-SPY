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
    // ---------------Building Prefabs-------------------
    [Header("BuildingData")]

    public List<Building> housePrefabs;
    public List<Building> shopPrefabs;
    public List<Building> factoryPrefabs;
    
    // ---------------Building Block List-----------------
    public List<BuildingBlock> empty_Building_Block_List;
    public List<BuildingBlock> built_Building_Block_List;
    // ---------------TrafficSignal List
    public List<TrafficSignal> trafficSignals_List;
}
