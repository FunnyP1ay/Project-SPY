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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    [Header("BuildingData")]
    [SerializeField]
    public List<Building> buildingPrefabs;


}
