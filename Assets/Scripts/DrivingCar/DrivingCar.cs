using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class DrivingCar : MonoBehaviour
{

    [SerializeField]
    private float speed;

    public Transform currentDrivingPoint;

    private void Start()
    {
        speed = 15f;
    }
    private void Update()
    {
        if(currentDrivingPoint != null)
        {
            gameObject.transform.LookAt(currentDrivingPoint);
            Vector3 moveVec = (currentDrivingPoint.position- transform.position ).normalized;
            transform.Translate(moveVec * speed * Time.deltaTime,Space.World);
        }
        
        
    }
    public void NextDrivingPoint(Transform _nextDrivingPoint)
    {
       currentDrivingPoint = _nextDrivingPoint;
    }
}
