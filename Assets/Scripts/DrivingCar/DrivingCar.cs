using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingCar : MonoBehaviour
{

    Transform currentDrivingPoint;

    private void Update()
    {
        gameObject.transform.LookAt(currentDrivingPoint);
        
    }
    public void NextDrivingPoint(Transform _nextDrivingPoint)
    {
       currentDrivingPoint = _nextDrivingPoint;
    }
}
