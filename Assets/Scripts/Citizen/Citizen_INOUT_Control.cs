using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GetInBuilding;

public class Citizen_INOUT_Control : MonoBehaviour
{
    private int randNum;
    private Citizen     citizen;
    public Transform    outPos;
    
    private void Awake()
    {
        citizen =  GetComponent<Citizen>();
    }
    public void  GetInBuilding()
    {
        int BuildingLayerMask = LayerMask.GetMask("Building");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2f, BuildingLayerMask);
        if (colliders.Length > 0)
        {
            randNum = Random.Range(0, colliders.Length);

            if (colliders[randNum].gameObject.TryGetComponent(out GetInBuilding _building))
                {
                    _building = colliders[randNum].gameObject.GetComponent<GetInBuilding>();

                    switch (_building.buildingDATA) 
                    {
                        case BuildingDATA.SuperMarket:
                        citizen.nav.Warp(MapData.Instance.playerSuperMarket_InPos.position);
                            this.gameObject.transform.position = MapData.Instance.playerSuperMarket_InPos.position;
                        _building.inCitizen_List.Add(this.gameObject);
                        this.gameObject.SetActive(false);
                        break;
                        case BuildingDATA.CoatStore:
                        citizen.nav.Warp(MapData.Instance.playerCoatStore_InPos.position);
                            this.gameObject.transform.position = MapData.Instance.playerCoatStore_InPos.position;
                        _building.inCitizen_List.Add(this.gameObject);
                        this.gameObject.SetActive(false);
                        break;
                        case BuildingDATA.PizzaStore:
                        citizen.nav.Warp(MapData.Instance.playerPizzaStore_InPos.position);
                            this.gameObject.transform.position = MapData.Instance.playerPizzaStore_InPos.position;
                        _building.inCitizen_List.Add(this.gameObject);
                        this.gameObject.SetActive(false);
                        break;
                        case BuildingDATA.FruitsStore:
                        citizen.nav.Warp(MapData.Instance.playerFruitsStore_InPos.position);
                            this.gameObject.transform.position = MapData.Instance.playerFruitsStore_InPos.position;
                        _building.inCitizen_List.Add(this.gameObject);
                        this.gameObject.SetActive(false);
                        break;
                        case BuildingDATA.PlayerHouse:
                            break;
                        default:
                            break;
                    }
                   
                    print(" 건물로 들어갔습니다!");
                }
            

        }
    }
    public void GetOutBuilding()
    {
        citizen.nav.Warp(outPos.position);
        this.gameObject.transform.position = outPos.position; 
        this.gameObject.SetActive(true);
        citizen.OutBuildingSetting();
        print("건물에서 나왔습니다 ! ");
    }

}
