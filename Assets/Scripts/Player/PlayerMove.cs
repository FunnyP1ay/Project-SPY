using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    // ------------- Player Setting -------------------
    public float            moveSpeed           = 5f;
    public float            jumpForce           = 50f;
    public float            rotationSpeed       = 100f;
    public float            currentHP           = 500;
    private float           fireRate            = 1f;
    private float           nextFireTime        = 0f;

    // --------------- Player State Check --------------
    [Header("Player State Check")]
    public bool             isBrokenAttack      = false;
    public bool             isGetIn             = false;
    public bool             isPlayerBuilding_In = false;
    public bool             isCoatChange        = false;
    private bool            isOpen              = false;

    // --------------- Player Script -------------------
    [Header("Player Scripts")]
    public SPYTargetObject      spy_Target_Object   = null;
    public SPYAction            spyAction;
    public WeaponControl        weaponControl;
    public Player_Cinemachine_Control player_Cinemachine_Control;
    public Animator             animator;

    public Transform        weaponPos;
    private Vector2         inputVector;
    private Vector3         moveVector;

    private void Start()
    {
        weaponControl = GetComponent<WeaponControl>();
        spyAction = GetComponent<SPYAction>();
        spy_Target_Object = GetComponent<SPYTargetObject>();
        player_Cinemachine_Control = GetComponent<Player_Cinemachine_Control>();
        animator = GetComponent<Animator>();
    }
    public void PlayerFire()
    {
        if (weaponControl.currentWeapon.gameObject.TryGetComponent(out GunFire _gun))
        {
            _gun.Fire();
            spyAction.ExposedAction(10f);
            player_Cinemachine_Control.Fire_Impulse();
        }
    }
    void Update()
    {
        //회전 보정 코드인데 없는게 더 나은거 같아서 임시로 삭제 
        //---------------------------------------------------------
        //float rotateAmount = moveVector.x * rotationSpeed;
        //transform.Rotate(new Vector3( 0f, 0f + rotateAmount, 0f) ) ;
        //---------------------------------------------------------
        // 이동 처리
        transform.Translate( moveVector.normalized * Time.deltaTime * moveSpeed);
        // 인풋매니저로 수정 해야 할듯 함.
        if(Input.GetMouseButton(0)&& weaponControl.weaponState == WeaponControl.WeaponState.equip&& Time.time >= nextFireTime)
        {

                animator.SetTrigger("isFire");
                nextFireTime = Time.time + 1f / fireRate;
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            weaponControl.WeaponChange(0);
            weaponControl.weaponState = WeaponControl.WeaponState.none;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            weaponControl.WeaponChange(1);
            weaponControl.weaponState = WeaponControl.WeaponState.equip;
        }
        if(Input.GetKey(KeyCode.Alpha3))
        {
            weaponControl.weaponState = WeaponControl.WeaponState.skill;
            weaponControl.WeaponChange(2);
        }
        if (Input.GetKey(KeyCode.F) && isBrokenAttack)
        {
            spyAction.BrokenObjectAttack(spy_Target_Object);
            if (player_Cinemachine_Control.iszoomSPYAction == false)
                StartCoroutine(player_Cinemachine_Control.zoomSPYAction());

        }
        if (Input.GetKeyDown(KeyCode.F) && isGetIn)
        {
            if (isPlayerBuilding_In)
            {
                isPlayerBuilding_In = false;
                this.transform.position = MapData.Instance.player_OutPos.position;
            }
            else
            { // 경찰이 따라오고 있을 때 건물에 들어갈려한다면
                if (isCoatChange == true)
                {
                    int police = LayerMask.GetMask("Police");
                    Collider[] colliders = Physics.OverlapSphere(transform.position, 45f, police);
                    if (colliders.Length > 0)
                    {
                        foreach (Collider collider in colliders)
                        {
                            collider.GetComponent<Police>().nav.SetDestination(MapData.Instance.player_OutPos.position);
                            collider.GetComponent<Police>().moveTarget = Police.MoveTarget.road;
                        }
                    }
                }
                isPlayerBuilding_In = true;
                this.transform.position = MapData.Instance.player_InPos.position;
            }
        }
        if (Input.GetKey(KeyCode.F) && isCoatChange)
        {
            spyAction.ChangeCoat(false); // is Coat UI Coat Change Icon :false
            UI_Manager.Instance.ui_PoliceIcon.PoliceIconSetting(-3);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            print("Tab키가 눌렸습니다");
            if (isOpen == false)
            {
                print("패널을 열었습니다");
                isOpen = true;
                UI_Manager.Instance.currentCityBuildingTax.text         = ("BuildingTax :")     + CityControlData.Instance.building_Tax.ToString();
                UI_Manager.Instance.currentCityCitizenTax.text          = ("CitizenTax :")      + CityControlData.Instance.citizen_Tax.ToString();
                UI_Manager.Instance.currentCityBuildingCount.text       = ("CityBuilding : ")   + MapData.Instance.built_Building_Block_List.Count.ToString();
                UI_Manager.Instance.currentMayor_Approval_Rating.text   = ("APProval Rating")   + CityControlData.Instance.approval_Rating.ToString();
                UI_Manager.Instance.currentCitizenCount.text            = ("CitizenCount : ")   + MapData.Instance.currentCitizenCount.ToString();
                UI_Manager.Instance.currentSafety_Rating.text           = ("Safety Rating : ")  + CityControlData.Instance.safety_Rating.ToString();
                UI_Manager.Instance.cityINFOPanel.SetActive(true);
                UI_Manager.Instance.ui_LawListPanel.gameObject.SetActive(true);
                UI_Manager.Instance.ui_LawListPanel.LawList_Setting();
            }
            else if (isOpen == true)
            {
                print("패널을 닫았습니다.");
                isOpen = false;
                UI_Manager.Instance.ui_LawListPanel.gameObject.SetActive(false);
                UI_Manager.Instance.cityINFOPanel.SetActive(false);
            }
        }
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        inputVector = value.ReadValue<Vector2>();

        // 입력 벡터를 이동 벡터로 변환
        moveVector = new Vector3(inputVector.x, moveVector.y , inputVector.y);
        if (moveVector.magnitude > 0.1f)
        {
            animator.SetBool("isMove", true);
            animator.SetFloat("inputVectorX", inputVector.x);
            animator.SetFloat("inputVectorY", inputVector.y);
        }
        else
            animator.SetBool("isMove", false);
    }
    public void GetDamage(float _damage)
    {
        currentHP -= _damage;
        if (currentHP < 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // 죽을 때 애니매이션 이나 게임 종료 !

    }
}