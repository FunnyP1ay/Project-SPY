using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class DrivingCar : MonoBehaviour
{

    [SerializeField]
    private float speed;
    [SerializeField]
    private float raycastRange = 3f;
    [SerializeField]
    private LayerMask targetLayer;
    
    public Transform currentDrivingPoint;
   // private bool waitMode = false;
   // private bool waitCoroutine = true;

    private void Start()
    {
        speed = 15f;
        raycastRange = 5f;
    }
    private void Update()
    {
        if(currentDrivingPoint != null)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position,transform.forward, out hit, raycastRange, targetLayer)) 
            {
          
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Citizen") 
                  /*  hit.collider.gameObject.layer == LayerMask.NameToLayer("DrivingCar")*/)
                {
                    DrivingStop();
         
                }
            }
            else
            {
                Driving();
            }
        }
    }
    public void NextDrivingPoint(Transform _nextDrivingPoint)
    {
       currentDrivingPoint = _nextDrivingPoint;
    }

    private void Driving() 
    {
        speed = 50f;
        gameObject.transform.LookAt(currentDrivingPoint);
        Vector3 moveVec = (currentDrivingPoint.position - transform.position).normalized;
        transform.Translate(moveVec * speed * Time.deltaTime, Space.World);
    }
    private void DrivingStop()
    {
            speed = 0f;
    }
    /*private IEnumerator WaitNextDrivingCoroutine()
    {
        waitMode = true;
        yield return new WaitForSecondsRealtime(3f);
        waitCoroutine = false;
        waitMode = false;
        yield break;
    }*/
}
