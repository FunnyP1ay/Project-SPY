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
    Animator                animator;
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
        animator = GetComponentInParent<Animator>();
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
            animator.SetFloat("isMove", nav.speed); // 애니매이션 세팅

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
        animator.SetFloat("isMove", nav.speed);

    }
    private void CheckRoadTargetPos()
    {
        if (navTarget == null)
        {
            moveState = MoveState.needNextMove;
            return;
        }
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
            switch (moveTarget) // 행동 구현하기
            {
                case MoveTarget.building:
                    CityControlData.Instance.safety_Rating += 0.1f;
                    break;
                case MoveTarget.store:
                    CityControlData.Instance.safety_Rating += 0.04f;
                    break;
                case MoveTarget.house:
                    CityControlData.Instance.safety_Rating += 0.02f;
                    break;
            }

            nav.updatePosition = false;
            moveState = MoveState.needNextMove;
        }
    }
    private void CheckSpyTargetPos()
    {
        checkDistance = Vector3.Distance(gameObject.transform.position, navTarget.transform.position);

        // 요약 : if (checkDistance < chaseRange) 에 있으면 사격 , 아니면 추적만,   else if (checkDistance > 35f) 이면 추격 종료 
        if (checkDistance < chaseRange) // TODO 밸런스 조절 제일 필요한 부분  
        {
            // TODO 경찰이 이정도 범위 일 때 총을 사용할지 체포만 할지 정하기
            nav.speed = 4f;
            if(navTarget.gameObject.TryGetComponent(out PlayerMove _player)/*SPY AI 추가하기*/)
            {
                if (_player.weaponControl.weaponState == WeaponControl.WeaponState.pistol)
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
       
            while (isFire)
            {
                animator.SetTrigger("isFire");
                FirePose();
                yield return new WaitForSecondsRealtime(0.5f);
            }
            isFire = false;
            yield break;
    }

    public void FirePose()
    {
        if (weaponControl.currentWeapon.gameObject.TryGetComponent(out GunFire _gun))
        {
            //transform.LookAt(navTarget.position);
            _gun.gameObject.transform.LookAt(navTarget.position);
            _gun.Fire();
        }
        else
        {
            weaponControl.weaponState = WeaponControl.WeaponState.pistol;
            weaponControl.WeaponChange(1); // equip weapon
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
        animator.SetFloat("isMove", nav.speed);
    }
    private void SetNavTarget_Building()
    {
        int BuildingLayerMask = LayerMask.GetMask("Building");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, BuildingLayerMask); //, BuildingLayerMask
        if (colliders.Length > 0)
        {
            randNum = Random.Range(0, colliders.Length);

            if (colliders[randNum].gameObject.TryGetComponent(out Building _building))
            {
                navTarget = _building.building_NavTargetPoint;
                nav.SetDestination(navTarget.position);
            }
        }
    }
    private void SetNavTarget_Road()
    {
        int roadLayerMask = LayerMask.GetMask("Road");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, roadLayerMask);
        if (colliders.Length > 0)
        {
            randNum = Random.Range(0, colliders.Length);
            if (colliders[randNum].gameObject.TryGetComponent(out Road _road))
            {
                randNum = Random.Range(0, _road.navTargetPos_List.Count);
                navTarget = _road.navTargetPos_List[randNum].transform;
                nav.SetDestination(navTarget.position);
            }
        }
    }

    public void ChaseSpy(Transform _target)
    {
        weaponControl.weaponState = WeaponControl.WeaponState.pistol;
        weaponControl.WeaponChange(1); // equip weapon
        
        moveTarget  = MoveTarget.spy;
        moveState       = MoveState.Move;
        nav.speed = 6f;
        nav.updatePosition = true;
        navTarget = _target;
        nav.SetDestination(navTarget.position);
        surprised_Mark.SetActive(true);
    }
    //TODO 경찰이 플레이어 일정 수준 이상으로 왔을 때, 총을 쏘거나 체포.
    public void ChaseFailed()
    {
        weaponControl.weaponState = WeaponControl.WeaponState.phone;
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


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out DrivingCar _car))
        {
            animator.SetTrigger("isKnockback");
        }
    }

    public void GetDamage(float _damage)
    {
        surprised_Mark.SetActive(true);
        ChaseSpy(MapData.Instance.chasePlayer_Pos);
        print("데미지를 입었습니다 ! ");
        currentHP -= _damage;
        if(currentHP < 0)
        {
            animator.SetTrigger("isDie");
        }
    }

    public void Die()
    {
        UI_Manager.Instance.ui_PoliceIcon.PoliceIconSetting(3);
        MapData.Instance.curretPoliceCount--;
        LeanPool.Despawn(this);
    }

}
