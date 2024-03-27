using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

    public Transform            building_NavTargetPoint;
    public List<GameObject>     inCitizen_List;

    public void Setting()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "NavTarget")
                building_NavTargetPoint = child;
        }
    }
}
