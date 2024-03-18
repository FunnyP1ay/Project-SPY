using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenSpawner : MonoBehaviour
{
    public GameObject   citizenPrefab;
    private int         randNum;
    private void Start()
    {
        
    }
    public void CitizenSpawn() 
    {
        if (MapData.Instance.built_Building_Block_List.Count > 0)
        {
            randNum = Random.Range(0, MapData.Instance.built_Building_Block_List.Count);
            var SpawnCitizen = MapData.Instance.empty_Building_Block_List[randNum];

            SpawnBuilding.BuildingSpawn(SpawnBuilding.transform);
        }

    }
}
