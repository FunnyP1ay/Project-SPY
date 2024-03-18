using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityControlData : MonoBehaviour
{
    private static CityControlData instance;
    public static CityControlData Instance { get { if (instance == null) return null; return instance; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(gameObject);
    }
    // ----------- Costs -----------------
    public int      cost_Building    = 100;
    public int      cost_Store       = 40;
    public int      cost_House       = 25;
    // ----------- CITY Tax --------------
    public int      citizen_Tax      = 0;
    public int      building_Tax     = 0;
    // ------- Mayor Approval Rating------
    public float    approval_Rating  = 90;
    public bool TakeBuildingTax(int value)
    {
        if (building_Tax >= value)
        {
            building_Tax -= value;
            return true;
        }
        else
            return false;  
    }
    public bool TakeCitizenTax(int value)
    {
        if (citizen_Tax >= value)
        {
            citizen_Tax -= value;
            return true;
        }
        else
            return false;
    }
}
