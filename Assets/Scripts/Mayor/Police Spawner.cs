using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceSpawner : MonoBehaviour
{
    public GameObject policePrefab;
    private int randNum;

    private void Start()
    {
        StartCoroutine(PoliceSpawnControl());
    }
    IEnumerator PoliceSpawnControl()
    {
        while (MapData.Instance.curretPoliceCount < MapData.Instance.maxPoliceCount)
        {
            PoliceSpawn();
            print("정부에서 경찰을 추가 배치 했습니다 !" + MapData.Instance.curretPoliceCount);
            yield return new WaitForSecondsRealtime(75f);
        }
        yield break;
    }

    public void FirstSpawn()
    {
        for (int i = 0; i < MapData.Instance.startPoliceCount; i++)
        {
            PoliceSpawn();
        }
        
    }
    public void PoliceSpawn()
    {
        if (MapData.Instance.built_Building_Block_List.Count > 0)
        {
            print("경찰이 배치 되었습니다 ! ");

            randNum = Random.Range(0, MapData.Instance.built_Building_Block_List.Count);


            var spawnPolice = LeanPool.Spawn(policePrefab).GetComponent<Police>();
            spawnPolice.transform.position = MapData.Instance.built_Building_Block_List[randNum].currentPrefab.building_NavTargetPoint.position;
            spawnPolice.nav.speed = 3.0f;
            spawnPolice.moveState = Police.MoveState.needNextMove;
            spawnPolice.PoliceSetting();
            spawnPolice.SetName();
            MapData.Instance.curretPoliceCount++;
        }
    }
    public void OperationsPoliceSpawn()
    {
        var spawnPolice = LeanPool.Spawn(policePrefab).GetComponent<Police>();
        spawnPolice.transform.position = MapData.Instance.policeCenterPos.transform.position;

        spawnPolice.moveState = Police.MoveState.Move;
        spawnPolice.moveTarget = Police.MoveTarget.OperationsTarget;
        
        spawnPolice.nav.SetDestination(MapData.Instance.chasePlayer_Pos.position);
        spawnPolice.nav.speed = 7.0f;
        spawnPolice.SetName();
        spawnPolice.PoliceSetting();
        MapData.Instance.curretPoliceCount++;
    }
}
