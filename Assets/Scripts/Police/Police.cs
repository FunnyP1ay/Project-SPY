using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Police : MonoBehaviour
{
   
    [SerializeField]
    Transform               navTarget;
   
    CitizenINFO             citizenINFO;
    WeaponControl           weaponControl;
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
    private void Start()
    {
        SetName();
    }
    public enum MoveState
    {
        die,
        needNextMove,
        Move
    }
    public MoveState moveState;

    public enum MoveTarget
    {
        building,
        store,
        house,
        road,
        spy
    }
    public MoveTarget moveTarget;


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
                CityControlData.Instance.safety_Rating += 0.01f; // ��ġ ����, �ִ밪 ���� �ʿ�
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
            moveState = MoveState.needNextMove;
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
            // TODO �ù��� ���� ������ �̵� ��Ű�� �� �����ϱ� 
            nav.updatePosition = false;
            moveState = MoveState.needNextMove;
        }
    }
    private void CheckSpyTargetPos()
    {
        checkDistance = Vector3.Distance(gameObject.transform.position, navTarget.transform.position);
        if (checkDistance < chaseRange) // TODO �뷱�� ���� ���� �ʿ��� �κ� 
        {
            // TODO ������ ������ ���� �� �� ���� ������� ü���� ���� ���ϱ�
            nav.speed = 2f;
            if(navTarget.gameObject.TryGetComponent(out PlayerMove _player)/*SPY AI �߰��ϱ�*/)
            {
                if (_player.weaponControl.weaponState == WeaponControl.WeaponState.equip)
                {
                    weaponControl.WeaponChange(1); // weapon equip
                    isFire = true;
                    StartCoroutine(Fire());
                }
            }
            nav.SetDestination(navTarget.position);
            isFire = false;
        }
        else if (checkDistance > 40f)
        {
            print("�߰ݿ� ���� �߽��ϴ� ! ");
            ChaseFailed();
            isFire = false;
            nav.speed = 3f;
        }
        else
            nav.SetDestination(navTarget.position);

    }
    public IEnumerator Fire()
    {
        if (weaponControl.currentWeapon.gameObject.TryGetComponent(out GunFire _gun))
        {
            while (isFire)
            {
                print("������ ����մϴ�");
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
                randNum = Random.Range(0, _building.building_NavTargetPoint.Count);
                navTarget = _building.building_NavTargetPoint[randNum].transform;
                nav.SetDestination(navTarget.position);
                nav.updatePosition = true;
                moveState = MoveState.Move;
            }
        }
    }
    private void SetNavTarget_Road()
    {
        int roadLayerMask = LayerMask.GetMask("Road");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, roadLayerMask);
        if (colliders.Length == 0)
        {
            nav.SetDestination(MapData.Instance.empty_Building_Block_List[0].transform.position);
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
        PoliceIconControl(1);
        moveTarget  = MoveTarget.spy;
        moveState       = MoveState.Move;
        nav.updatePosition = true;
        navTarget = _target;
        nav.SetDestination(navTarget.position);
    }
    //TODO ������ �÷��̾� ���� ���� �̻����� ���� ��, ���� ��ų� ü��.
    public void ChaseFailed()
    {
        weaponControl.weaponState = WeaponControl.WeaponState.none;
        weaponControl.WeaponChange(0); // none weapon
        moveTarget  = MoveTarget.road;
        moveState       = MoveState.needNextMove;
        PoliceIconControl(-3);
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
        print("�������� �Ծ����ϴ� ! ");
        currentHP -= _damage;
        if(currentHP < 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // ���� �� �ִϸ��̼� 
        MapData.Instance.curretPoliceCount--;
        LeanPool.Despawn(this);
    }

}
