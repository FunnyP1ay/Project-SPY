using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MayorsSpawnControl : MonoBehaviour
{
    // TODO Police Spawner
    public BuildingSpawner      buildingSpawner;
    public CitizenSpawner       citizenSpawner;
    public PoliceSpawner        policeSpawner;
    private void Start()
    {
        buildingSpawner =   GetComponent<BuildingSpawner>();
        citizenSpawner  =   GetComponent<CitizenSpawner>();
        policeSpawner   =   GetComponent<PoliceSpawner>();

        buildingSpawner.FirstSpawn();
        citizenSpawner.FirstSpawn();
        policeSpawner.FirstSpawn();
    }

    public void Build_Building()
    {
        if (CityControlData.Instance.TakeBuildingTax(CityControlData.Instance.cost_Building))
        {
            buildingSpawner.BuildingSpawn(0); // 0 : Building
            print("정치인이 빌딩을 지었습니다 ! ");
        }
    }
    public void Build_Store()
    {
        if (CityControlData.Instance.TakeBuildingTax(CityControlData.Instance.cost_Store))
        {
            buildingSpawner.BuildingSpawn(1); // 1 : Store
            print("정치인이 상점을 지었습니다 !");
        }
    }
    public void Build_House()
    {
        if (CityControlData.Instance.TakeBuildingTax(CityControlData.Instance.cost_House))
        {
            buildingSpawner.BuildingSpawn(2); // 2 : House
            print("정치인이 집을 공급했습니다 ! ");
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
