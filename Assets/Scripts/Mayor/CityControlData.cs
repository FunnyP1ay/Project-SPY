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
    [Header("Mayor Approval Rating")]
    // ----------- Mayor Approval Rating-----
    public float approval_Rating = 90.0f;
    [Header("BuildCost")]
    // ----------- Costs --------------------
    public int      cost_Building    = 500;
    public int      cost_Store       = 200;
    public int      cost_House       = 100;
    [Header("CITY Tax")]
    // ----------- CITY Tax -----------------
    public int      citizen_Tax      = 0;
    public int      building_Tax     = 0;
    [Header("CITY Law List")]
    // ----------- CITY Law List ------------
    public bool     martial_Law;
    public bool     travel_Law;
    public bool     romance_Law;
    public bool     game_Law;
    public bool     prohibition_Law; // ±ÝÁÖ·É
    public bool     cigarette_Law;
    public bool     pets_Law;
    public bool     tax_Law;

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
