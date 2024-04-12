using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{
    public  int     startBuildingCount = 10 ;
    public  int     startStoreCount = 30 ;
    public  int     startHouseCount = 80 ;

    private int     randNum;
    public void FirstSpawn()
    {
        for (int i = 0; i < startBuildingCount; i++)
        {
            BuildingSpawn(0); // Building
        }
        for (int i = 0; i < startStoreCount; i++)
        {
            BuildingSpawn(1); // Store
        }
        for (int i = 0; i < startHouseCount; i++)
        {
            BuildingSpawn(2); // House
        }
    }

    public void BuildingSpawn(int _value)  // 1: Building, 2: Store, 3: House
    {
        if (MapData.Instance.empty_Building_Block_List.Count > 0)
        {
            randNum = Random.Range(0, MapData.Instance.empty_Building_Block_List.Count);
            var SpawnBuilding = MapData.Instance.empty_Building_Block_List[randNum];
            MapData.Instance.empty_Building_Block_List.RemoveAt(randNum);
            MapData.Instance.built_Building_Block_List.Add(SpawnBuilding);
            SpawnBuilding.BuildingSpawn(SpawnBuilding.transform, _value);
        }
        else
        {
            print("더이상 건물을 지을 곳이 없습니다");
        }
    }
}
