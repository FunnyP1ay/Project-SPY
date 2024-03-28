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

    // �÷��̾ ���� �� ���������� �ȿ� �ִ� �ֺ� ��� �ù��� ����Ʈ�� �ֱ�
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
                _citizen.InBuildingSetting();// �ù� �ٽ� Ű�� �� ����


            }
        }
    }
    // �ڷ�ƾ ���鼭 10�ʿ� �ѹ��� �׷���, if(�÷��̾ �ش��ϴ� ���� �ȿ� ������) ���ο� ���� �ùε��� �� ���� �ù��� �Ա��� �̵���Ű�� ������
    // �÷��̾ �ٽ� ���� ���¸� �׾ȿ� �ִ� �ùε� ��� �ٽ� ����Ʈ�� ���� ����
}
