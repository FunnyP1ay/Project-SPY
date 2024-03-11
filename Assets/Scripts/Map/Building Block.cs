using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Analytics;
using UnityEngine;

public class BuildingBlock : MonoBehaviour
{
    int randNum;
    public void BuildingSpawn(Transform _blockPos)
    {
        randNum = Random.Range(0, MapData.Instance.buildingPrefabs.Count);
        var newBuilding = LeanPool.Spawn(MapData.Instance.buildingPrefabs[randNum]);
        newBuilding.transform.position = _blockPos.position;
        newBuilding.transform.SetParent(gameObject.transform);
    }
}
