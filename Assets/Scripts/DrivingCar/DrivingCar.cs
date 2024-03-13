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
    private void Start()
    {
        setSpeed = 50f;
        currentSpeed = setSpeed;
        raycastRange = 5f;
    }
    private void Update()
    {
        if(currentDrivingPoint != null)
        {
            RaycastHit hit;
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
        print("잠시 멈췄습니다 ! ");
    }
    private void DrivingSlowStart()
    {
        currentSpeed = setSpeed / 2;
        print("천천히 운행합니다 ! ");
    }
    private void DrivingReStart()
    {
        currentSpeed = setSpeed;
        print("다시 운행합니다 ! ");
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
