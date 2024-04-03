using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityControlData : MonoBehaviour
{
    private static CityControlData instance;
    public static CityControlData Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    [Header("Mayor Approval Rating")]
    // ----------- Mayor Approval Rating-----
    public float approval_Rating = 90.0f;

    [Header("Safety Rating")]
    // ----------- Safety Rating ------------
    public float safety_Rating = 90.0f;

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
    // -----Bad Law
    public bool     martial_Law;
    public bool     travel_Law;
    public bool     romance_Law;
    public bool     game_Law;
    public bool     prohibition_Law; // ±ÝÁÖ·É
    public bool     cigarette_Law;
    public bool     pets_Law;
    public bool     tax_Law;
    // -----Good Law
    public bool     sportEvent_Law;
    public bool     freeHair_Law;
    public bool     gym_Law;
    public bool     parade_Law;
    public bool     mayorsMovie_Law;
    public bool     hospital_Law;
    // -----Comic Law : approval Rating Not Change
    public bool     marshmallow_Law;
    public bool     sleep_Law;
    public bool     chocolate_Law;
    public bool     pizza_Law;
    public bool     goodMorning_Law;
    public bool     rockScissorsPaper_Law;
    public bool     cola_Law;
    public bool     handstand_Law;
    public bool     brushingTeeth_Law;
    // --------------------------------------
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
    public bool LawStateChange(bool _value)
    {
        return !_value;
    }
    public void SetApprovalRating(float _value)
    {
        approval_Rating += _value;
    }
    public void ResetDATA()
    {
        approval_Rating = 90.0f;
        safety_Rating = 90.0f;
        citizen_Tax = 0;
        building_Tax = 0;

        martial_Law = false;
        travel_Law = false;
        romance_Law = false;
        game_Law = false;
        prohibition_Law = false;
        cigarette_Law = false;
        pets_Law = false;
        tax_Law = false;

        sportEvent_Law = false;
        freeHair_Law = false;
        gym_Law = false;
        parade_Law = false;
        mayorsMovie_Law = false;
        hospital_Law = false;
    }
}
