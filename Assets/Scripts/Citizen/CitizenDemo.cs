using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenDemo : MonoBehaviour
{
    Citizen         citizen;
    CitizenINFO     citizenINFO;
    void Awake()
    {
        citizen = GetComponent<Citizen>();
        citizenINFO = GetComponent<CitizenINFO>();
    }


    public void Demo()
    {

    }
}
