using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1.5f, BuildingLayerMask);
        if (colliders.Length > 0)
        {
            randNum = Random.Range(0, colliders.Length);

            if (colliders[randNum].gameObject.TryGetComponent(out GetInBuilding _building))
            {
                    _building = colliders[randNum].gameObject.GetComponent<GetInBuilding>();

                    switch (_building.buildingDATA) 
                    {
                        case BuildingDATA.SuperMarket:
                        citizen.nav.enabled = false; // 네비메쉬 에이전트 비활성화
                        this.gameObject.transform.position = MapData.Instance.playerSuperMarket_InPos.position;
                        citizen.nav.Warp(this.gameObject.transform.position);
                        _building.inCitizen_List.Add(this.gameObject);
                        this.gameObject.SetActive(false);
                        break;
                        case BuildingDATA.CoatStore:
                        citizen.nav.enabled = false; // 네비메쉬 에이전트 비활성화
                        this.gameObject.transform.position = MapData.Instance.playerCoatStore_InPos.position;
                        citizen.nav.Warp(this.gameObject.transform.position);
                        _building.inCitizen_List.Add(this.gameObject);
                        this.gameObject.SetActive(false);
                        break;
                        case BuildingDATA.PizzaStore:
                        citizen.nav.enabled = false; // 네비메쉬 에이전트 비활성화
                        this.gameObject.transform.position = MapData.Instance.playerPizzaStore_InPos.position;
                        citizen.nav.Warp(this.gameObject.transform.position);
                        _building.inCitizen_List.Add(this.gameObject);
                        this.gameObject.SetActive(false);
                        break;
                        case BuildingDATA.FruitsStore:
                        citizen.nav.enabled = false; // 네비메쉬 에이전트 비활성화
                        this.gameObject.transform.position = MapData.Instance.playerFruitsStore_InPos.position;
                        citizen.nav.Warp(this.gameObject.transform.position);
                        _building.inCitizen_List.Add(this.gameObject);
                        this.gameObject.SetActive(false);
                        break;
                        case BuildingDATA.PlayerHouse:
                        citizen.state = Citizen.State.needNextMove;
                            break;
                        default:
                        citizen.state = Citizen.State.needNextMove;
                        break;
                    }
                   
                    print(" 건물로 들어갔습니다!");
            }
            else
            {
                citizen.state = Citizen.State.needNextMove;
            }
            

        }
        else
        {
            citizen.state = Citizen.State.needNextMove;
        }
    }
    public void GetOutBuilding()
    {
        this.gameObject.transform.position = outPos.position;
        this.gameObject.SetActive(true);
        if(citizen.nav.enabled ==false)
        citizen.nav.enabled = true;

        citizen.nav.Warp(outPos.position); // 네비메쉬 에이전트의 위치를 업데이트
        citizen.OutBuildingSetting();
        print("건물에서 나왔습니다 ! ");
    }

}
