using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Analytics;
using UnityEngine;

public class BuildingBlock : MonoBehaviour
{
    int randNum;
    float setVector;
    Transform target;
    Collider[] colliders;
    public LayerMask layerMask;
    private void Awake()
    {
        setVector = 99999f;
        colliders = Physics.OverlapSphere(transform.position,20f, layerMask);
        if (colliders.Length == 0)
        {
            print($"{name}의 타깃이 없습니다 !");
        }
        else
        {
            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out Road road))
                {
                    float calDistance = Vector3.Distance(transform.position, road.transform.position);
                    if (setVector > calDistance)
                    {
                        setVector = calDistance;
                        target = road.transform;
                        print("타겟을 잡았습니다.");
                    }


                }
            }
        }
    }
    public void BuildingSpawn(Transform _blockPos)
    {

            randNum = Random.Range(0, MapData.Instance.housePrefabs.Count);
            var newBuilding = LeanPool.Spawn(MapData.Instance.housePrefabs[randNum]);
            newBuilding.transform.position = _blockPos.position;
            newBuilding.transform.SetParent(gameObject.transform);
            newBuilding.transform.LookAt(target);
        
    }
}
