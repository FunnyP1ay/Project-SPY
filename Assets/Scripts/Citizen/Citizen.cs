using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Citizen : MonoBehaviour
{
    [SerializeField]
    Transform           navTarget;
    public NavMeshAgent        nav;
    CitizenINFO         citizenINFO;
    private int         randNum;
    private int         randNum2;
    private int         randNum3;
    private float       checkDistance;
    public string       citizenName;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        citizenINFO = GetComponent<CitizenINFO>();
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
        house,
        road,
    }
    public MoveTarget moveTarget; 

    public IEnumerator MoveCoroutine()
    {
        while (state != State.die)
        {
            if (state == State.needNextMove)
            {
                SetNextMoveTarget();
            }
            if (state == State.Move)
            {
                CheckTargetPos();
            }
            yield return new WaitForSecondsRealtime(3f);
        }
        yield break;
    }
    // --------- Check Target Position 
    private void CheckTargetPos()
    {
        switch (moveTarget)
        {
            case MoveTarget.building:
                CheckBuildingTargetPos();
                break;
            case MoveTarget.store:
                CheckBuildingTargetPos();
                break;
            case MoveTarget.house:
                CheckBuildingTargetPos();
                break;
            case MoveTarget.road:
                CheckRoadTargetPos();
                break;
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
        switch (randNum)
        {
            case 0:
                SetNavTarget_Building(0); // Building
                break;
            case 1:
                SetNavTarget_Building(1); // Store
                break;
            case 2:
                SetNavTarget_Building(2); // House
                break;
            default:
                SetNavTarget_Road();      // Road
                break;
        }
    }
    private void SetNavTarget_Building(int _value)
    {
        int BuildingLayerMask = LayerMask.GetMask("Building");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 30f, BuildingLayerMask);
        
        if(colliders.Length == 0)
        {
            SetNavTarget_Road();
            print("건물이 없습니다 !");
        }
        else
        {
            switch (_value) // TODO 각 건물 별 행동 패턴 구현
            {
                case 0:
                    moveTarget = MoveTarget.building;
                    print("타겟을 빌딩으로 잡았습니다 ! ");
                    break;
                case 1:
                    moveTarget = MoveTarget.store;
                    print("타겟을 상점으로 잡았습니다 ! ");
                    break;
                case 2:
                    moveTarget = MoveTarget.house;
                    print("타겟을 주택으로 잡았습니다 ! ");
                    break;
            }
            randNum = Random.Range(0, colliders.Length);
            if (colliders[randNum].gameObject.TryGetComponent(out Building _building))
            {
                randNum = Random.Range(0, _building.building_NavTargetPoint.Count);
                navTarget = _building.building_NavTargetPoint[randNum].transform;
                nav.SetDestination(navTarget.position);
                nav.updatePosition = true;
                state = State.Move;
            }
        }
    }
    private void SetNavTarget_Road()
    {
        int roadLayerMask = LayerMask.GetMask("Road");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, roadLayerMask);
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
        citizenINFO.nameText.fontSize = 0.5f;
    }
}
