using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mayor : MonoBehaviour
{
    public MayorControll    mayorControll;
    private int             randNum;
    private int             randNum2;

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
    public enum Police_Contrills
    {
        none=0,
        police_Spawn=1,
        police_AllSpawn=2,
        police_Despawn=3,
    }
    public State state;
    public Build_Controlls build_Controlls;
    public Police_Contrills police_Controlls;

    void Start()
    {
        mayorControll = GetComponent<MayorControll>();
        StartCoroutine(CityControll());
    }
    private IEnumerator CityControll()
    {
        while(state == State.cityControll)
        {

            if((build_Controlls != Build_Controlls.none))
            {
                if(build_Controlls == Build_Controlls.build_Building)
                {

                }
                if(build_Controlls == Build_Controlls.build_Store)
                {

                }
                if(build_Controlls == Build_Controlls.build_House)
                {

                }
            }
            if( police_Controlls != Police_Contrills.none)
            {
                if(police_Controlls == Police_Contrills.police_Spawn)
                {

                }
                if(police_Controlls == Police_Contrills.police_AllSpawn)
                {

                }
                if(police_Controlls == Police_Contrills.police_Despawn)
                {

                }
            }
            yield return new WaitForSecondsRealtime(60f);
        }
        yield break;
    }
    public void NextOrder()
    {
        if (CityData.Instance.building_Tax > CityData.Instance.cost_Building)
        {
            randNum = Random.Range(0, 4);
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
        if (CityData.Instance.building_Tax > CityData.Instance.cost_Building)
        {
            randNum = Random.Range(0, 4);
            switch (randNum)
            {
                case 0:
                    police_Controlls = Police_Contrills.none;
                    break;
                case 1:
                    police_Controlls = Police_Contrills.police_Spawn;
                    break;
                case 2:
                    police_Controlls = Police_Contrills.police_AllSpawn;
                    break;
                case 3:
                    police_Controlls = Police_Contrills.police_Despawn;
                    break;
            }
        }
    }
}
