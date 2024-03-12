using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivePoint : MonoBehaviour
{
    List<DrivePoint> nextPoints;


    private void OnTriggerEnter(Collider collider)
    {
        collider.TryGetComponent(out DrivingCar nextPoint);
    }
}
