using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using Unity.VisualScripting;

public class CitizenSpawner : MonoBehaviour
{
    public GameObject   citizenPrefab;
    private int         randNum;

    private void Start()
    {
        StartCoroutine(CitizenSpawnControl());
    }
    IEnumerator CitizenSpawnControl()
    {
        while (MapData.Instance.currentCitizenCount < MapData.Instance.maxCitizenCount)
        {
            CitizenSpawn();
            print("시민이 새로 왔습니다 !" + MapData.Instance.currentCitizenCount);
            yield return new WaitForSecondsRealtime(75f);
        }
        yield break;
    }

    public void FirstSpawn()
    {
        for (int i = 0; i < MapData.Instance.built_Building_Block_List.Count*2; i++)
        {
            CitizenSpawn();
        }
    }
    public void CitizenSpawn() 
    {
        if (MapData.Instance.built_Building_Block_List.Count > 0)
        {
          
            randNum = Random.Range(0, MapData.Instance.built_Building_Block_List.Count);
            var spawnCitizen = LeanPool.Spawn(citizenPrefab).GetComponent<Citizen>();
            spawnCitizen.nav.speed = 3.0f;
            spawnCitizen.state = Citizen.State.needNextMove;
            spawnCitizen.SetName();
            spawnCitizen.nav.Warp(MapData.Instance.built_Building_Block_List[randNum].currentPrefab.building_NavTargetPoint.position);
            spawnCitizen.transform.position = MapData.Instance.built_Building_Block_List[randNum].currentPrefab.building_NavTargetPoint.position;
            spawnCitizen.SetName();
            spawnCitizen.CitizenCoroutineSetting();
            MapData.Instance.currentCitizenCount++;
        }
    }
    public void In_Building_Move_Setting()
    {

    }
}
