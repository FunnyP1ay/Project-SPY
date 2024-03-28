using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static GetInBuilding;

public class GetInBuilding : MonoBehaviour
{
    public List<GameObject> inCitizen_List;
    private int             randNum;
    public enum BuildingDATA
    {
        PlayerHouse,
        SuperMarket,
        CoatStore,
        PizzaStore,
        FruitsStore
    }
    public BuildingDATA buildingDATA;
    public Transform outBuildingPos;
    private void OnEnable()
    {
        outBuildingPos = transform.Find("GetInPos");
        StartCoroutine(CheckCitizen());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMove player))
        {
            UI_Manager.Instance.ui_Key_Icon_Action.F_Key_SetActive_True();
            MapData.Instance.player_OutPos = outBuildingPos;
            MapData.Instance.chasePlayer_Pos = outBuildingPos;
            player.currentGetInBuilding = this;
            switch (buildingDATA)
            {
                case BuildingDATA.PlayerHouse:
                    MapData.Instance.player_InPos = MapData.Instance.playerHouse_InPos;
                    break;
                case BuildingDATA.SuperMarket:
                    MapData.Instance.player_InPos = MapData.Instance.playerSuperMarket_InPos;
                    break;
                case BuildingDATA.CoatStore:
                    MapData.Instance.player_InPos = MapData.Instance.playerCoatStore_InPos;
                    break;
                case BuildingDATA.PizzaStore:
                    MapData.Instance.player_InPos = MapData.Instance.playerPizzaStore_InPos;
                    break;
                case BuildingDATA.FruitsStore:
                    MapData.Instance.player_InPos = MapData.Instance.playerFruitsStore_InPos;
                    break;
            }
            player.isGetIn = true;
        }
        if (other.gameObject.TryGetComponent(out Citizen_INOUT_Control citizen))
        {
            citizen.outPos = outBuildingPos;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMove player))
        {
            UI_Manager.Instance.ui_Key_Icon_Action.F_Key_SetActive_False();
            player.isGetIn = false;
            MapData.Instance.chasePlayer_Pos = player.transform;
        }
    }

    IEnumerator CheckCitizen()
    {
        while (true)
        {
            if (inCitizen_List.Count>0&& outBuildingPos != MapData.Instance.player_OutPos)
            {
                    randNum = Random.Range(0, inCitizen_List.Count);
                inCitizen_List[randNum].gameObject.GetComponent<Citizen_INOUT_Control>().GetOutBuilding();
                inCitizen_List.RemoveAt(randNum);
            }
            
            yield return new WaitForSecondsRealtime(20f);
        }
    }

    // 플레이어가 나갈 때 순간적으로 안에 있는 주변 모든 시민을 리스트에 넣기
    public void CitizenGetInList(Transform _player)
    {
        int citizen = LayerMask.GetMask("Citizen");
        Collider[] colliders = Physics.OverlapSphere(_player.position, 20f, citizen);
        foreach(var _citizen in colliders)
        {
            inCitizen_List.Add(_citizen.gameObject);
        }
    }
    public void CitizenSetActive()
    {
        if (inCitizen_List.Count > 0 )
        {
            foreach (var citizen in inCitizen_List)
            {
                var _citizen = citizen.gameObject.GetComponent<Citizen>();

                _citizen.gameObject.SetActive(true);
                _citizen.InBuildingSetting();// 시민 다시 키는 거 설정


            }
        }
    }
    // 코루틴 돌면서 10초에 한번씩 그러나, if(플레이어가 해당하는 빌딩 안에 있으면) 내부에 켜진 시민들을 중 나갈 시민을 입구로 이동시키고 나가기
    // 플레이어가 다시 나간 상태면 그안에 있는 시민들 어떻게 다시 리스트에 넣을 건지
}
