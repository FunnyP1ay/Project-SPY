using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mayor : MonoBehaviour
{
    public MayorControl    mayorControl;
    private int             randNum;
    private int             StateCount              = Enum.GetValues(typeof(State)).Length;
    private int             Build_ControlsCount    = Enum.GetValues(typeof(Build_Controls)).Length;
    private int             Police_ControlsCount   = Enum.GetValues(typeof(Police_Controls)).Length;
    public enum State
    {
        die,
        cityControl
    }
    public enum Build_Controls
    {
        none=0,
        build_Building=1,
        build_Store=2,
        build_House=3
    }
    public enum Police_Controls
    {
        none=0,
        police_Spawn=1,
        police_AllSpawn=2,
        martial_Law=3
    }
    public State                state;
    public Build_Controls      build_Controls;
    public Police_Controls     police_Controls;

    void Start()
    {
        mayorControl = GetComponent<MayorControl>();
        StartCoroutine(CityControl());
    }
    private IEnumerator CityControl()
    {
        while(state == State.cityControl)
        {   
            //---------------------Order Setting-----------------
            NextOrder();
            //---------------------Build_Orders------------------
            if ((build_Controls != Build_Controls.none))
            {
                switch (build_Controls)
                {
                    case Build_Controls.build_Building:
                        mayorControl.Build_Building();
                        break;
                    case Build_Controls.build_Store:
                        mayorControl.Build_Store();
                        break;
                    case Build_Controls.build_House:
                        mayorControl.Build_House();
                        break;
                }
            }
            // ---------------------Police_Orders-----------------------
            if ( police_Controls != Police_Controls.none)
            {
                switch (police_Controls)
                {
                    case Police_Controls.police_Spawn:
                        mayorControl.Police_Spawn();
                        break;
                    case Police_Controls.police_AllSpawn:
                        mayorControl.All_Police_Spawn();
                        break;
                    case Police_Controls.martial_Law:
                        mayorControl.Martial_law();
                        break;
                }
            }
            yield return new WaitForSecondsRealtime(60f);
        }
        yield break;
    }
    private void NextOrder()
    {
        // ---------------------Build_Orders------------------
        if (CityControlData.Instance.building_Tax > CityControlData.Instance.cost_Building)
        {
            randNum = UnityEngine.Random.Range(0, Build_ControlsCount);
            switch (randNum)
            {
                case 0:
                    build_Controls = Build_Controls.none;
                    break;
                case 1:
                    build_Controls = Build_Controls.build_Building;
                    break;
                case 2:
                    build_Controls = Build_Controls.build_Store;
                    break;
                case 3:
                    build_Controls = Build_Controls.build_House;
                    break;
            }
        }
        else
        {
            build_Controls = Build_Controls.none;
        }
        // ---------------------Police_Orders-----------------------

        if(CityControlData.Instance.approval_Rating <= 80) // 정치 지지율 80 이상인 정상 적인 상태
        {
            police_Controls = Police_Controls.none;
        }
        else
        {
            if(CityControlData.Instance.approval_Rating > 20)
            {
                police_Controls = Police_Controls.martial_Law; // 계엄령
            }
            else if(CityControlData.Instance.approval_Rating > 40)
            {
                police_Controls = Police_Controls.police_AllSpawn; // 경찰 인력 총동원
            }
            else if(CityControlData.Instance.approval_Rating > 60)
            {
                police_Controls = Police_Controls.police_Spawn; // 경찰 증원
            }
            else
            {
                police_Controls = Police_Controls.none;
                //TODO 미정 정치인의 60~80 사이의 추가 행동 구현 할 때 필요할듯
            }
        }
    }
}
