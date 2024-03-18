using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MayorControl : MonoBehaviour
{
    // TODO Police Spawner
    public BuildingSpawner      buildingSpawner;
    public CitizenSpawner       citizenSpawner;
    private void Start()
    {
        buildingSpawner =   GetComponent<BuildingSpawner>();
        citizenSpawner  =   GetComponent<CitizenSpawner>();

        buildingSpawner.FirstSpawn();
        citizenSpawner.FirstSpawn();
      
    }

    public void Build_Building()
    {
        if (CityControlData.Instance.TakeBuildingTax(CityControlData.Instance.cost_Building))
        {
            buildingSpawner.BuildingSpawn(0); // 0 : Building
        }
    }
    public void Build_Store()
    {
        if (CityControlData.Instance.TakeBuildingTax(CityControlData.Instance.cost_Store))
        {
            buildingSpawner.BuildingSpawn(1); // 1 : Store
        }
    }
    public void Build_House()
    {
        if (CityControlData.Instance.TakeBuildingTax(CityControlData.Instance.cost_House))
        {
            buildingSpawner.BuildingSpawn(2); // 2 : House
        }
    }
    public void Police_Spawn()
    {

    }
    public void All_Police_Spawn()
    {

    }
    public void Martial_law()
    {

    }
}
