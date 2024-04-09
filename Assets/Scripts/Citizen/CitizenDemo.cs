using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenDemo : MonoBehaviour
{
    Citizen                 citizen;
    CitizenINFO             citizenINFO;
    public GameObject       demoObject;
    void Awake()
    {
        citizen = GetComponent<Citizen>();
        citizenINFO = GetComponent<CitizenINFO>();
        demoObject.SetActive(false);
    }


    public void Demo()
    {
        citizen.animator.SetBool("isDemo", true);
        CityControlData.Instance.approval_Rating -= 0.01f;
        demoObject.SetActive(true);
    }
}
