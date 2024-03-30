using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SPYAction : MonoBehaviour
{
    public bool                 needChange = false;
    private int                 randNum;
    public List<GameObject>     coat_List;
    public GameObject           currentPlayerCoat;
    public Player_SkillAtack    player_SkillAtack;
    private void Start()
    {
        player_SkillAtack = GetComponent<Player_SkillAtack>();
        CoatSetting();
        StartCoroutine(NeedChageCoat());
    }
    public void BrokenObjectAttack(SPYTargetObject _target)
    {
        _target.OnAttack();
        ExposedAction(_target.exposedRange);
    }

    public void ExposedAction(float _range) //�Է°��� ���� �Ÿ� ����
    {
        int police = LayerMask.GetMask("Police");
        Collider[] policeColliders = Physics.OverlapSphere(transform.position, _range, police);
        if (policeColliders.Length > 0)
        {
            foreach (Collider collider in policeColliders)
            {
                if (collider.TryGetComponent(out Police _police))
                {
                    _police.ChaseSpy(transform);
                    _police.PoliceIconControl(1);
                }
            }
            ChangeCoatUI(true); // is Player Coat change UI True 
        }

        int citizen = LayerMask.GetMask("Citizen");
        Collider[] citizenColliders = Physics.OverlapSphere(transform.position, _range, citizen);
        if(citizenColliders.Length > 0)
        {
            foreach(Collider collider in citizenColliders)
            {
                collider.gameObject.GetComponent<Citizen>().RunAway();
            }
        }
    }
    public void ChangeCoat(bool _value)
    {
        //TODO Player Coat Change ������ ����
        print("Change Coat ! ");
        ChangeCoatUI(_value);
    }
    public void ChangeCoatUI(bool _value) // Police�� Player�� Ž�� ���� �� True���� ������.
    {
        if (_value == true)
        {
            needChange = true;
            UI_Manager.Instance.ui_Player_Coat_Icon.gameObject.SetActive(true);
            UI_Manager.Instance.ui_Player_Coat_Icon.enabled = true;
        }
        else
        {
            needChange = false;
            UI_Manager.Instance.ui_Player_Coat_Icon.gameObject.SetActive(false);
            UI_Manager.Instance.ui_Player_Coat_Icon.enabled = false;
            CoatSetting();
        }
    }
    public void CoatSetting()
    {
        randNum = Random.Range(0, coat_List.Count);
        currentPlayerCoat.SetActive(false);
        currentPlayerCoat = coat_List[randNum];
        currentPlayerCoat.SetActive(true);
    }
    public IEnumerator NeedChageCoat()
    {
        while (true)
        {
            //�ٷ�    ExposedAction(); �� ȣ���ϸ� ���ÿ��� �÷ο� �߻��ϹǷ� ���� ����
            yield return new WaitForSecondsRealtime(2f);
            if (needChange == true)
            {
                ExposedAction(10f);
            }
        }
    }
}
