using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingBlock : MonoBehaviour
{
    int                 randNum;
    float               setVector;
    Transform           target;
    Collider[]          colliders;
    public LayerMask    layerMask;
    public Building     currentPrefab;
    private void Awake()
    {
        currentPrefab = GetComponent<Building>();
        setVector = 99999f;
        colliders = Physics.OverlapSphere(transform.position,20f, layerMask);

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out Road road))
                {
                    float calDistance = Vector3.Distance(transform.position, road.transform.position);
                    if (setVector > calDistance)
                    {
                        setVector = calDistance;
                        target = road.transform;
                    }
                }
            }
    }
    public void BuildingSpawn(Transform _blockPos, int _value)
    {
       
        switch (_value)
        {
            case 0:
                randNum = Random.Range(0, MapData.Instance.buildingPrefabs.Count);
                var newBuilding = LeanPool.Spawn(MapData.Instance.buildingPrefabs[randNum]);
                currentPrefab = newBuilding;
                break;
            case 1:
                randNum = Random.Range(0, MapData.Instance.storePrefabs.Count);
                var newStore = LeanPool.Spawn(MapData.Instance.storePrefabs[randNum]);
                currentPrefab = newStore;
                break;
            case 2:
                randNum = Random.Range(0, MapData.Instance.housePrefabs.Count);
                var newHouse = LeanPool.Spawn(MapData.Instance.housePrefabs[randNum]);
                currentPrefab = newHouse;
                break;

        }
        currentPrefab.Setting();
        currentPrefab.transform.position = _blockPos.position;
        currentPrefab.transform.SetParent(gameObject.transform);
        currentPrefab.transform.LookAt(target);
        
    }
}
