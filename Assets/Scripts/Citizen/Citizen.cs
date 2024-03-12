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
    private int         randNum;
    float               checkDistance;

    // Start is called before the first frame update
    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();

        nav.speed = 3.0f;
        state = State.needNextMove;
        StartCoroutine(MoveCoroutine());
       
    }
    private void OnEnable()
    {
    }

    public enum State
    {
        die,
        freeMove,
        needNextMove
    }
    public State state;


    IEnumerator MoveCoroutine()
    {
        while(state != State.die)
        {
            
            if (state == State.freeMove)
            {
                CheckTargetPos();
            }
            if(state == State.needNextMove)
            {
                SetNavTarget();
            }
            yield return new WaitForSecondsRealtime(3f);
        }
        yield break;
    }

    private void SetNavTarget()
    {
        int roadLayerMask = LayerMask.GetMask("Road");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 15f, roadLayerMask);
        randNum = Random.Range(0, colliders.Length);
        if (colliders[randNum].gameObject.TryGetComponent(out Road _road))
        {
            randNum = Random.Range(0,_road.navTargetPos_List.Count);
            navTarget =  _road.navTargetPos_List[randNum].transform;
            nav.SetDestination(navTarget.position);
            nav.updatePosition = true;
            state = State.freeMove;
        }

    }
    private void CheckTargetPos()
    {
        checkDistance = Vector3.Distance(gameObject.transform.position, navTarget.transform.position);
        if (checkDistance < 2f)
        {
            nav.updatePosition = false;
            state = State.needNextMove;
        }

    }
    private void CrossTheCrosswalk()
    {

    }
    private void TrafficTarget()
    {

    }

}
