using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CitizenINFO : MonoBehaviour
{
    Transform               cam;
    Citizen                 citizen;
    public TextMeshProUGUI  nameText; 
    void Start()
    {
        cam = Camera.main.transform;
        citizen = GetComponent<Citizen>();
        nameText.text = citizen.citizenName;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + cam.rotation * Vector3.forward, cam.rotation * Vector3.up);
    
    }
}
