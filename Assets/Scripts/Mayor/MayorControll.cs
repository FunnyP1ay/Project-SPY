using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MayorControll : MonoBehaviour
{
    // TODO Police Spawner
    public BuildingSpawner buildingSpawner;
    
    private void Start()
    {
        buildingSpawner = GetComponent<BuildingSpawner>();
    }

    public void Build_Building()
    {
        if (CityData.Instance.TakeBuildingTax(CityData.Instance.cost_Building))
        {
            buildingSpawner.BuildingSpawn(0); // 0 : Building
        }
        else
        {
            //TODO 불가 UI 띄우던가 하기 (근데 AI가 컨트롤 하고 있기 때문에 스스로 알지 않을 까 ..)
        }
    }
    public void Build_Store()
    {
        if (CityData.Instance.TakeBuildingTax(CityData.Instance.cost_Store))
        {
            buildingSpawner.BuildingSpawn(1); // 1 : Store
        }
        else
        {
            //TODO 불가 UI 띄우던가 하기 (근데 AI가 컨트롤 하고 있기 때문에 스스로 알지 않을 까 ..)
        }
    }
    public void Build_House()
    {
        if (CityData.Instance.TakeBuildingTax(CityData.Instance.cost_House))
        {
            buildingSpawner.BuildingSpawn(2); // 2 : House
        }
        else
        {
            //TODO 불가 UI 띄우던가 하기 (근데 AI가 컨트롤 하고 있기 때문에 스스로 알지 않을 까 ..)
        }
    }
    public void Police_Spawn()
    {

    }
    public void All_Poice_Spawn()
    {

    }
    public void Martial_law()
    {

    }
}
