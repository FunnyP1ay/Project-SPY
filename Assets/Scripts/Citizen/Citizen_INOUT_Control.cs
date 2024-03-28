using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GetInBuilding;

public class Citizen_INOUT_Control : MonoBehaviour
{
    private Citizen     citizen;
    public Transform    outPos; // GetInBuilding 스크립트에서 트리거에 들어올 때마다 정해줌 
    private void Awake()
    {
        citizen =  GetComponent<Citizen>();
    }
    public void  GetInBuilding()
    {
        int BuildingLayerMask = LayerMask.GetMask("Building");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 4f, BuildingLayerMask);
        if (colliders.Length > 0)
        {
            foreach (Collider collider in colliders)
            {
                if(collider.TryGetComponent(out GetInBuilding _building))
                {
                    _building = colliders[0].GetComponent<GetInBuilding>();

                    switch (_building.buildingDATA) // 각자 건물에 맞는 위치로 이동시키기 
                    {
                        case BuildingDATA.SuperMarket:
                            this.gameObject.transform.position = MapData.Instance.playerSuperMarket_InPos.position;
                            break;
                        case BuildingDATA.CoatStore:
                            this.gameObject.transform.position = MapData.Instance.playerCoatStore_InPos.position;
                            break;
                        case BuildingDATA.PizzaStore:
                            this.gameObject.transform.position = MapData.Instance.playerPizzaStore_InPos.position;
                            break;
                        case BuildingDATA.FruitsStore:
                            this.gameObject.transform.position = MapData.Instance.playerFruitsStore_InPos.position;
                            break;
                        case BuildingDATA.PlayerHouse:
                            break;
                        default:
                            break;
                    }
                    _building.inCitizen_List.Add(this.gameObject);
                    this.gameObject.SetActive(false);
                    print("건물 안으로 들어왔습니다 !");
                }
            }

        }
    }
    public void GetOutBuilding()
    {
        this.gameObject.SetActive(true);
        this.gameObject.transform.position = outPos.position;
        citizen.OutBuildingSetting();
        print("건물 밖으로 나왔습니다.");
    }

}
