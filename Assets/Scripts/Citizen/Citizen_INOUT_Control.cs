using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GetInBuilding;

public class Citizen_INOUT_Control : MonoBehaviour
{
    public Transform outPos; // GetInBuilding 스크립트에서 트리거에 들어올 때마다 정해줌 
    public void  GetInBuilding()
    {
        int BuildingLayerMask = LayerMask.GetMask("Building");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 4f, BuildingLayerMask);
        if (colliders.Length > 0)
        {
            var _building = colliders[0].GetComponent<Building>();
            this.gameObject.transform.position = MapData.Instance.playerPizzaStore_InPos.position;
            _building.inCitizen_List.Add(this.gameObject);
            this.gameObject.SetActive(false);
            print("건물 안으로 들어왔습니다 !");
        }
    }
    public void GetOutBuilding()
    {

    }
    
    void Start()
    {
        
    }


}
