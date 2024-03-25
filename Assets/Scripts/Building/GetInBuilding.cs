using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static GetInBuilding;

public class GetInBuilding : MonoBehaviour
{
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
    private void Start()
    {
        outBuildingPos = transform.Find("GetInPos");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMove player))
        {
            UI_Manager.Instance.ui_Key_Icon_Action.F_Key_SetActive_True();
            MapData.Instance.player_OutPos = outBuildingPos;
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
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMove player))
        {
            UI_Manager.Instance.ui_Key_Icon_Action.F_Key_SetActive_False();
            player.isGetIn = false;
        }
    }
}
