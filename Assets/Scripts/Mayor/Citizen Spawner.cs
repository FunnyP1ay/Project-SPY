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
            yield return new WaitForSecondsRealtime(60f);
        }
        yield break;
    }

    public void FirstSpawn()
    {
        for (int i = 0; i < MapData.Instance.built_Building_Block_List.Count; i++)
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
            spawnCitizen.transform.position = MapData.Instance.built_Building_Block_List[randNum].currentPrefab.building_NavTargetPoint[0].transform.position;
            spawnCitizen.nav.speed = 3.0f;
            spawnCitizen.state = Citizen.State.needNextMove;
            spawnCitizen.StartCoroutine(spawnCitizen.MoveCoroutine());
            spawnCitizen.SetName();
            MapData.Instance.currentCitizenCount++;
                print("시민 생성완료" + MapData.Instance.currentCitizenCount);
            
        }
    }
}
