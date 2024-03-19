using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Citizen;

public class Police : MonoBehaviour
{
   
    [SerializeField]
    Transform           navTarget;
    NavMeshAgent        nav;
    CitizenINFO         citizenINFO;
    WeaponControl      weaponControl;
    private int         randNum;
    private int         randNum2;
    private int         randNum3;
    private float       checkDistance;
    public string       citizenName;

    private void Awake()
    {
        weaponControl = GetComponent<WeaponControl>();
        nav = GetComponent<NavMeshAgent>();
        citizenINFO = GetComponent<CitizenINFO>();
    }
    private void Start()
    {
        SetName();
    }
    private void OnEnable()
    {
        nav.speed = 3.0f;
        state = MoveState.needNextMove;
        StartCoroutine(MoveCoroutine());
    }

    public enum MoveState
    {
        die,
        needNextMove,
        Move
    }
    public MoveState state;

    public enum MoveTarget
    {
        building,
        store,
        house,
        road,
        spy
    }
    public MoveTarget moveTarget;


    IEnumerator MoveCoroutine()
    {
        while (state != MoveState.die)
        {
            if (state == MoveState.needNextMove)
            {
                SetNextMoveTarget();
            }
            if (state == MoveState.Move)
            {
                CheckTargetPos();
            }
            yield return new WaitForSecondsRealtime(3f);
        }
        yield break;
    }
    // --------- Check Target Position -----------
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
                case MoveTarget.spy:
                CheckSpyTargetPos();
                break;
        }
    }
    private void CheckRoadTargetPos()
    {
        checkDistance = Vector3.Distance(gameObject.transform.position, navTarget.transform.position);
        if (checkDistance < 2f)
        {
            nav.updatePosition = false;
            state = MoveState.needNextMove;
        }
    }
    private void CheckBuildingTargetPos()
    {
        checkDistance = Vector3.Distance(gameObject.transform.position, navTarget.transform.position);
        if (checkDistance < 2f)
        {
            switch (moveTarget)
            {
                case MoveTarget.building:
                    break;
                case MoveTarget.store:
                    break;
                case MoveTarget.house:
                    break;
            }
            // TODO 시민을 빌딩 안으로 이동 시키는 거 구현하기 
            nav.updatePosition = false;
            state = MoveState.needNextMove;
        }
    }
    private void CheckSpyTargetPos()
    {
        checkDistance = Vector3.Distance(gameObject.transform.position, navTarget.transform.position);
        if (checkDistance < 10f) // TODO 밸런스 조절 제일 필요한 부분 
        {
            // TODO 경찰이 이정도 범위 일 때 총을 사용할지 체포만 할지 정하기
            nav.speed = 3f;
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
                SetNavTarget_Building(); // Building
                break;
            case 1:
                SetNavTarget_Building(); // Store
                break;
            case 2:
                SetNavTarget_Building(); // House
                break;
            default:
                SetNavTarget_Road();      // Road
                break;
        }
    }
    private void SetNavTarget_Building()
    {
        int BuildingLayerMask = LayerMask.GetMask("Building");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 30f, BuildingLayerMask);

        if (colliders.Length == 0)
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
                state = MoveState.Move;
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
            state = MoveState.Move;
            moveTarget = MoveTarget.road;
        }
    }

    public void ChaseSpy(Transform _target)
    {
        weaponControl.weaponState = WeaponControl.WeaponState.equip;
        weaponControl.WeaponChange(1); // equip weapon
        moveTarget  = MoveTarget.spy;
        state       = MoveState.Move;
        nav.updatePosition = true;
        nav.SetDestination(_target.position);
    }
    //TODO 경찰이 플레이어 일정 수준 이상으로 왔을 때, 총을 쏘거나 체포.
    public void ChaseFailed()
    {
        weaponControl.weaponState = WeaponControl.WeaponState.none;
        weaponControl.WeaponChange(0); // none weapon
        moveTarget  = MoveTarget.road;
        state       = MoveState.needNextMove;

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
