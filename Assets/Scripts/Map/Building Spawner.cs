using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{


    [SerializeField]
    private List<BuildingBlock>    empty_Buiding_Block_List;

    int randNum;

    public void BuildingSpawn()
    {
        randNum = Random.Range(0, empty_Buiding_Block_List.Count);
        var SpawnBuilding = empty_Buiding_Block_List[randNum];
        SpawnBuilding.BuildingSpawn(SpawnBuilding.transform);
    }
}
