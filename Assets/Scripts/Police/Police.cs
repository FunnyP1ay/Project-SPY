using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Citizen;

public class Police : MonoBehaviour
{
   
    [SerializeField]
    Transform               navTarget;
   
    CitizenINFO             citizenINFO;
    WeaponControl           weaponControl;
    public GameObject       question_Mark;
    public GameObject       surprised_Mark;
    public  NavMeshAgent    nav;
    private int             randNum;
    private int             randNum2;
    private int             randNum3;
    private float           checkDistance;
    public float            chaseRange;
    public string           citizenName;
    public bool             isFire = false;
    public float            currentHP = 100f;

    private void Awake()
    {
        weaponControl = GetComponent<WeaponControl>();
        nav = GetComponent<NavMeshAgent>();
        citizenINFO = GetComponent<CitizenINFO>();
    }
    public enum MoveState
    {
        die,
        needNextMove,
        Move,
        None
    }
    public MoveState moveState = MoveState.None;

    public enum MoveTarget
    {
        building,
        store,
        house,
        road,
        spy,
        OperationsTarget,
        None

    }
    public MoveTarget moveTarget = MoveTarget.None;


    public void PoliceSetting()
    {
        StartCoroutine(MoveCoroutine());
    }
    public IEnumerator MoveCoroutine()
    {
        while (moveState != MoveState.die)
        {
            if (moveState == MoveState.needNextMove)
            {
                SetNextMoveTarget();
            }
            if (moveState == MoveState.Move)
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
                CityControlData.Instance.safety_Rating += 0.01f; // 수치 조절, 최대값 설정 필요
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
                case MoveTarget.OperationsTarget:
                CheckOperations();
                break;
            default:
                break;
        }
    }
    private void CheckRoadTargetPos()
    {
        checkDistance = Vector3.Distance(gameObject.transform.position, navTarget.transform.position);
        if (checkDistance < 2f)
        {
            nav.updatePosition = false;
            moveState = MoveState.needNextMove;
        }
    }
    private void CheckBuildingTargetPos()
    {
        checkDistance = Vector3.Distance(gameObject.transform.position, navTarget.transform.position);
        if (checkDistance < 3f)
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
            moveState = MoveState.needNextMove;
        }
    }
    private void CheckSpyTargetPos()
    {
        checkDistance = Vector3.Distance(gameObject.transform.position, navTarget.transform.position);
        if (checkDistance < chaseRange) // TODO 밸런스 조절 제일 필요한 부분 
        {
            // TODO 경찰이 이정도 범위 일 때 총을 사용할지 체포만 할지 정하기
            nav.speed = 4f;
            if(navTarget.gameObject.TryGetComponent(out PlayerMove _player)/*SPY AI 추가하기*/)
            {
                if (_player.weaponControl.weaponState == WeaponControl.WeaponState.equip)
                {
                    weaponControl.WeaponChange(1); // weapon equip
                    isFire = true;
                    StartCoroutine(Fire());
                }
            }
            else
            {
                ChaseFailed();
                isFire = false;
            }

            nav.SetDestination(navTarget.position);
            isFire = false;
        }
        else if (checkDistance > 35f)
        {
            print("추격에 실패 했습니다 ! ");
            ChaseFailed();
            isFire = false;
            
        }
        else
            nav.SetDestination(navTarget.position);

    }

    private void CheckOperations()
    {
        navTarget = MapData.Instance.chasePlayer_Pos;
        checkDistance = Vector3.Distance(gameObject.transform.position, navTarget.transform.position);
        if (checkDistance < chaseRange)
        {
            moveTarget = MoveTarget.spy;
        }
        else
        {
            nav.SetDestination(navTarget.position);
        }
    }
    public IEnumerator Fire()
    {
        if (weaponControl.currentWeapon.gameObject.TryGetComponent(out GunFire _gun))
        {
            while (isFire)
            {
                print("경찰이 사격합니다");
                transform.LookAt(navTarget.position);
                _gun.Fire();
                yield return new WaitForSecondsRealtime(0.5f);
            }
            isFire = false;
            yield break;
        }
        else
            isFire = false;
        yield break;
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
        Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, BuildingLayerMask);

        if (colliders.Length == 0)
        {
            SetNavTarget_Road();
        }
        else
        {
            randNum = Random.Range(0, colliders.Length);
            if (colliders[randNum].gameObject.TryGetComponent(out Building _building))
            {
                navTarget = _building.building_NavTargetPoint;
                nav.SetDestination(navTarget.position);
                nav.updatePosition = true;
                moveState = MoveState.Move;
            }
        }
    }
    private void SetNavTarget_Road()
    {
        int roadLayerMask = LayerMask.GetMask("Road");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 40f, roadLayerMask);
        if (colliders.Length == 0)
        {
            nav.SetDestination(MapData.Instance.NavMesh_Target_Bug_Fix_Pos.position);
            nav.updatePosition = true;
            moveState = MoveState.Move;
            moveTarget = MoveTarget.road;
        }
        else 
        { 
            randNum = Random.Range(0, colliders.Length);
            if (colliders[randNum].gameObject.TryGetComponent(out Road _road))
            {
                randNum = Random.Range(0, _road.navTargetPos_List.Count);
                navTarget = _road.navTargetPos_List[randNum].transform;
                nav.SetDestination(navTarget.position);
                nav.updatePosition = true;
                moveState = MoveState.Move;
                moveTarget = MoveTarget.road;
            }
        }
    }

    public void ChaseSpy(Transform _target)
    {
        weaponControl.weaponState = WeaponControl.WeaponState.equip;
        weaponControl.WeaponChange(1); // equip weapon
        
        moveTarget  = MoveTarget.spy;
        moveState       = MoveState.Move;
        nav.updatePosition = true;
        navTarget = _target;
        nav.SetDestination(navTarget.position);
        surprised_Mark.SetActive(true);
    }
    //TODO 경찰이 플레이어 일정 수준 이상으로 왔을 때, 총을 쏘거나 체포.
    public void ChaseFailed()
    {
        weaponControl.weaponState = WeaponControl.WeaponState.none;
        weaponControl.WeaponChange(0); // none weapon
        moveTarget  = MoveTarget.road;
        moveState       = MoveState.needNextMove;
        nav.speed = 3f;
        PoliceIconControl(-1);

        surprised_Mark.SetActive(false);
    }
    //------------------Police Icon Control ------------------
    public void PoliceIconControl(int _value)
    {
        UI_Manager.Instance.ui_PoliceIcon.PoliceIconSetting(_value);
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

    public void GetDamage(float _damage)
    {
        surprised_Mark.SetActive(true);
        ChaseSpy(MapData.Instance.chasePlayer_Pos);
        print("데미지를 입었습니다 ! ");
        currentHP -= _damage;
        if(currentHP < 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // 죽을 때 애니매이션 
        UI_Manager.Instance.ui_PoliceIcon.PoliceIconSetting(3);
        MapData.Instance.curretPoliceCount--;
        LeanPool.Despawn(this);
    }

}
