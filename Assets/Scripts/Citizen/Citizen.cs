using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Citizen : MonoBehaviour
{
    [SerializeField]
    Transform navTarget;
    NavMeshAgent nav;
    CitizenINFO citizenINFO;
    private int randNum;
    private int randNum2;
    private int randNum3;
    private float checkDistance;
    public string citizenName;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        citizenINFO = GetComponent<CitizenINFO>();
    }
    // Start is called before the first frame update
    private void Start()
    {

        nav.speed = 3.0f;
        state = State.needNextMove;
        StartCoroutine(MoveCoroutine());
        SetName();


    }
    private void OnEnable()
    {
    }

    public enum State
    {
        die,
        needNextMove,
        Move
    }
    public State state;

    public enum MoveTarget
    {
        building,
        store,
        road,
    }
    public MoveTarget moveTarget; 

    IEnumerator MoveCoroutine()
    {
        while (state != State.die)
        {

            if (state == State.Move)
            {
                CheckTargetPos();
            }
            if (state == State.needNextMove)
            {
                SetNextMoveTarget();
            }
            yield return new WaitForSecondsRealtime(3f);
        }
        yield break;
    }
    // --------- Check Target Position 
    private void CheckTargetPos()
    {
        if(moveTarget == MoveTarget.road)
        {
            CheckRoadTargetPos();
        }
        else if(moveTarget == MoveTarget.building)
        {
            CheckBuildingTargetPos();
        }
    }
   
    private void CheckRoadTargetPos()
    {
        checkDistance = Vector3.Distance(gameObject.transform.position, navTarget.transform.position);
        if (checkDistance < 2f)
        {
            nav.updatePosition = false;
            state = State.needNextMove;
        }

    }
    private void CheckBuildingTargetPos()
    {
        checkDistance = Vector3.Distance(gameObject.transform.position, navTarget.transform.position);
        if (checkDistance < 2f)
        {
            // TODO 시민을 빌딩 안으로 이동 시키는 거 구현하기 
            nav.updatePosition = false;
            state = State.needNextMove;
        }

    }
    //------------------Move Target Setting -----------------
    private void SetNextMoveTarget()
    {
        // next move target value setting
        randNum = Random.Range(0, 10);
        if(randNum > 3)
        {
            SetNavTarget_Building();
        }
        else
        {
            SetNavTarget_Road();
        }
    }
    private void SetNavTarget_Building()
    {
        int roadLayerMask = LayerMask.GetMask("Building");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 15f, roadLayerMask);
        if(colliders.Length == 0)
        {
            SetNavTarget_Road();
        }
        else
        {
            randNum = Random.Range(0, colliders.Length);
            if (colliders[randNum].gameObject.TryGetComponent(out Building _building))
            {
                randNum = Random.Range(0, _building.building_NavTargetPoint.Count);
                navTarget = _building.building_NavTargetPoint[randNum].transform;
                nav.SetDestination(navTarget.position);
                nav.updatePosition = true;
                state = State.Move;
                moveTarget = MoveTarget.building;
            }
        }
    }
    private void SetNavTarget_Road()
    {
        int roadLayerMask = LayerMask.GetMask("Road");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 15f, roadLayerMask);
        randNum = Random.Range(0, colliders.Length);
        if (colliders[randNum].gameObject.TryGetComponent(out Road _road))
        {
            randNum = Random.Range(0, _road.navTargetPos_List.Count);
            navTarget = _road.navTargetPos_List[randNum].transform;
            nav.SetDestination(navTarget.position);
            nav.updatePosition = true;
            state = State.Move;
            moveTarget = MoveTarget.road;
        }

    }
    private void CrossTheCrosswalk()
    {

    }
    private void TrafficTarget()
    {

    }
    public void SetName()
    {
        randNum = Random.Range(0, GameDB.Instance.nameChar.Count);
        randNum2 = Random.Range(0, GameDB.Instance.nameChar_2.Count);
        randNum3 = Random.Range(0, GameDB.Instance.nameChar_2.Count);
        citizenName = GameDB.Instance.nameChar[randNum].ToString() +
                        GameDB.Instance.nameChar_2[randNum2].ToString() +
                            GameDB.Instance.nameChar_2[randNum3].ToString();

        citizenINFO.nameText.text = citizenName;
        citizenINFO.nameText.fontSize = 1;
    }
}
