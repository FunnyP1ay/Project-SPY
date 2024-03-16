using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

    public List<Transform> building_NavTargetPoint = new List<Transform>();
    public List<Citizen>   building_In_Citizen = new List<Citizen>();

    void Start()
    {
      
        foreach (Transform child in transform)
        {
          
            if (child.gameObject.name == "NavTarget")
            {
              
                building_NavTargetPoint.Add(child);
            }
        }
    }

}
