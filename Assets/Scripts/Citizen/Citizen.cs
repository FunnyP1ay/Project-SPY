using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Citizen : MonoBehaviour
{
    [SerializeField]
    Transform           navTarget;
    NavMeshAgent        nav;
   
    // Start is called before the first frame update
    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        nav.speed = 1.0f;
        nav.updatePosition = true;

        nav.SetDestination(navTarget.position);
        StartCoroutine(SetTargetCoroutine());
    }

    public enum State
    {
        die,
        move,
        needNextMove
    }
    public State state;


    private IEnumerator SetTargetCoroutine()
    {
        while(state != State.die)
        {
            if(state == State.needNextMove)
            {
                SetNavTarget();
            }
            yield return new WaitForSeconds(3f);
        }

        yield break;
    }
    private void SetNavTarget()
    {
        Collider[] collider = Physics.OverlapSphere(Vector3.zero, 20f);
        //collider.

    }
    private void CrossTheCrosswalk()
    {

    }
    private void TrafficTarget()
    {

    }



}
