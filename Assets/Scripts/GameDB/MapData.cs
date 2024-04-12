using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class MapData : MonoBehaviour
{
    private static MapData instance;
    public static MapData Instance { get { return instance; } }
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
        DontDestroyOnLoad(gameObject);
    }
    // --------------- Building Prefabs List -------------------
    [Header("BuildingPrefabs")]

    public List<Building>           housePrefabs;
    public List<Building>           storePrefabs;
    public List<Building>           buildingPrefabs;
    public List<Building>           factoryPrefabs;
    // --------------- Building Block List  ---------------------
    [Header("Block DATA")]
    public List<BuildingBlock>      empty_Building_Block_List;
    public List<BuildingBlock>      built_Building_Block_List;
    // ---------------      Citizen DATA    ----------------------
    [Header("Citizen DATA")]
    public int                      currentCitizenCount  = 0;
    public int                      maxCitizenCount      = 200;
    // ---------------     Police DATA      ----------------------
    [Header("Police DATA")]
    public int                      startPoliceCount     = 30;
    public int                      curretPoliceCount;
    public int                      maxPoliceCount       = 80;
    public Transform                policeCenterPos;
    public Transform                chasePlayer_Pos;
    // --------------- NavMesh Target Bug Fix Pos ---------
    public Transform                NavMesh_Target_Bug_Fix_Pos;
    // ---------------  TrafficSignal List  ----------------------
    [Header("TrafficSignal")]
    public List<TrafficSignal>      trafficSignals_List;
    // ---------------  Player House DATA -------------------------
    [Header("Player IN OUT DATA")]
    public Transform player_InPos;
    public Transform player_OutPos;
    [Header("Player InStore DATA")]
    public Transform playerHouse_InPos;
    public Transform playerSuperMarket_InPos;
    public Transform playerCoatStore_InPos;
    public Transform playerPizzaStore_InPos;
    public Transform playerFruitsStore_InPos;
    
    public void ResetDATA()
    {


        empty_Building_Block_List.Clear();
        built_Building_Block_List.Clear();

        currentCitizenCount = 0;
        curretPoliceCount = 0;
        player_InPos = null;
    }
    
}
