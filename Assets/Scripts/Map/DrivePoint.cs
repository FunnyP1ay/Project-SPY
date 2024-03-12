using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivePoint : MonoBehaviour
{
    [SerializeField]
    List<DrivePoint> nextPoints;
    
    int randNum;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.TryGetComponent(out DrivingCar drivingCar))
        {
            randNum = Random.Range(0, nextPoints.Count);

            drivingCar.NextDrivingPoint(nextPoints[randNum].transform);
        }
    }
}
