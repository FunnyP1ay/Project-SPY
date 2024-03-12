using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{

    int randNum;

    public void BuildingSpawn()
    {
        randNum = Random.Range(0, MapData.Instance.empty_Building_Block_List.Count);
        var SpawnBuilding = MapData.Instance.empty_Building_Block_List[randNum];
        MapData.Instance.empty_Building_Block_List.RemoveAt(randNum);
        MapData.Instance.built_Building_Block_List.Add(SpawnBuilding);
        SpawnBuilding.BuildingSpawn(SpawnBuilding.transform);
        
    }
}
