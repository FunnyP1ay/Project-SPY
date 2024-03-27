using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class DrivingCar : MonoBehaviour
{

    [SerializeField]
    private float           setSpeed;
    private float           currentSpeed;
    [SerializeField]
    private float           raycastRange = 5f;
    [SerializeField]
    private LayerMask       targetLayer;
    
    public Transform        currentDrivingPoint;
    public bool             doingCourutine = false;
    RaycastHit              hit;
    private void Start()
    {
        currentSpeed = setSpeed;
    }
    private void Update()
    {
        if(currentDrivingPoint != null)
        {
            //TODO 최적화 생각해보기 
            if(Physics.Raycast(transform.position,transform.forward, out hit, raycastRange, targetLayer)) 
            {
          
                if ( hit.collider.gameObject.layer == LayerMask.NameToLayer("Citizen") ||
                hit.collider.gameObject.layer == LayerMask.NameToLayer("DrivingCar"))
                {
                    if(doingCourutine == false)
                    {
                        doingCourutine=true;
                        StartCoroutine(WaitCoroutine());
                    }
                }
            }

            Driving();
        }
    }
    public void NextDrivingPoint(Transform _nextDrivingPoint)
    {
       currentDrivingPoint = _nextDrivingPoint;
    }

    private void Driving() 
    {
        gameObject.transform.LookAt(currentDrivingPoint);
        Vector3 moveVec = (currentDrivingPoint.position - transform.position).normalized;
        transform.Translate(moveVec * currentSpeed * Time.deltaTime, Space.World);
    }
    private void DrivingStop()
    {
        
        currentSpeed = 0f;
    }
    private void DrivingSlowStart()
    {
        currentSpeed = setSpeed / 2;
    }
    private void DrivingReStart()
    {
        currentSpeed = setSpeed;
    }
    private IEnumerator WaitCoroutine()
    {
        DrivingStop();
        yield return new WaitForSecondsRealtime(0.5f);
        DrivingSlowStart();
        yield return new WaitForSecondsRealtime(1.5f);
        DrivingReStart();
        doingCourutine = false;
        yield break;
    }
}
