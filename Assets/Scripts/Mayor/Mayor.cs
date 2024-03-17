using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mayor : MonoBehaviour
{
    public MayorControll    mayorControll;
    private int             randNum;
    private int             StateCount              = Enum.GetValues(typeof(State)).Length;
    private int             Build_ControllsCount    = Enum.GetValues(typeof(Build_Controlls)).Length;
    private int             Police_ControllsCount   = Enum.GetValues(typeof(Police_Controlls)).Length;
    public enum State
    {
        die,
        cityControll
    }
    public enum Build_Controlls
    {
        none=0,
        build_Building=1,
        build_Store=2,
        build_House=3
    }
    public enum Police_Controlls
    {
        none=0,
        police_Spawn=1,
        police_AllSpawn=2,
        martial_Law=3
    }
    public State                state;
    public Build_Controlls      build_Controlls;
    public Police_Controlls     police_Controlls;

    void Start()
    {
        mayorControll = GetComponent<MayorControll>();
        StartCoroutine(CityControll());
    }
    private IEnumerator CityControll()
    {
        while(state == State.cityControll)
        {
            NextOrder();
            //---------------------Build_Orders------------------
            if ((build_Controlls != Build_Controlls.none))
            {
                switch (build_Controlls)
                {
                    case Build_Controlls.build_Building:
                        mayorControll.Build_Building();
                        break;
                    case Build_Controlls.build_Store:
                        mayorControll.Build_Store();
                        break;
                    case Build_Controlls.build_House:
                        mayorControll.Build_House();
                        break;
                }
            }
            // ---------------------Police_Orders-----------------------
            if ( police_Controlls != Police_Controlls.none)
            {
                switch (police_Controlls)
                {
                    case Police_Controlls.police_Spawn:
                        mayorControll.Police_Spawn();
                        break;
                    case Police_Controlls.police_AllSpawn:
                        mayorControll.All_Poice_Spawn();
                        break;
                    case Police_Controlls.martial_Law:
                        mayorControll.Martial_law();
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
        if (CityData.Instance.building_Tax > CityData.Instance.cost_Building)
        {
            randNum = UnityEngine.Random.Range(0, Build_ControllsCount);
            switch (randNum)
            {
                case 0:
                    build_Controlls = Build_Controlls.none;
                    break;
                case 1:
                    build_Controlls = Build_Controlls.build_Building;
                    break;
                case 2:
                    build_Controlls = Build_Controlls.build_Store;
                    break;
                case 3:
                    build_Controlls = Build_Controlls.build_House;
                    break;
            }
        }
        else
        {
            build_Controlls = Build_Controlls.none;
        }
        // ---------------------Police_Orders-----------------------

        if(CityData.Instance.approval_Rating <= 80) // 정치 지지율 80 이상인 정상 적인 상태
        {
            police_Controlls = Police_Controlls.none;
        }
        else
        {
            if(CityData.Instance.approval_Rating > 20)
            {
                police_Controlls = Police_Controlls.martial_Law; // 계엄령
            }
            else if(CityData.Instance.approval_Rating > 40)
            {
                police_Controlls = Police_Controlls.police_AllSpawn; // 경찰 인력 총동원
            }
            else if(CityData.Instance.approval_Rating > 60)
            {
                police_Controlls = Police_Controlls.police_Spawn; // 경찰 증원
            }
            else
            {
                police_Controlls = Police_Controlls.none;
                //TODO 미정 정치인의 60~80 사이의 추가 행동 구현 할 때 필요할듯
            }
        }
    }
}
