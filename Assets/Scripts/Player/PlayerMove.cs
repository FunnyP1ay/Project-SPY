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
    public SPYTargetObject  spy_Target_Object   = null;
    public SPYAction        spyAction;
    public WeaponControl    weaponControl;


    private Vector2         inputVector;
    private Vector3         moveVector;



    private void Start()
    {
        weaponControl = GetComponent<WeaponControl>();
        spyAction = GetComponent<SPYAction>();
        spy_Target_Object = GetComponent<SPYTargetObject>();
    }
    void Update()
    {
        float rotateAmount = moveVector.x * rotationSpeed;
        transform.Rotate(new Vector3( 0f, 0f + rotateAmount, 0f) ) ;
        // 이동 처리
        transform.Translate( moveVector.normalized * Time.deltaTime * moveSpeed);
        // 인풋매니저로 수정 해야 할듯 함.
        if(Input.GetMouseButton(0)&& weaponControl.weaponState == WeaponControl.WeaponState.equip&& Time.time >= nextFireTime)
        {
            if(weaponControl.currentWeapon.gameObject.TryGetComponent(out GunFire _gun))
            {
                _gun.Fire();
                nextFireTime = Time.time + 1f / fireRate;
            }
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
        }
        if (Input.GetKeyDown(KeyCode.F) && isGetIn)
        {
            if (isPlayerBuilding_In)
            {
                isPlayerBuilding_In = false;
                this.transform.position = MapData.Instance.player_OutPos.position;
            }
            else
            {
                isPlayerBuilding_In = true;
                this.transform.position = MapData.Instance.playerHouse_InPos.position;
            }
        }
        if (Input.GetKey(KeyCode.F) && isCoatChange)
        {
            spyAction.ChangeCoat(false); // is Coat UI Coat Change Icon :false
        }
        if (Input.GetKeyDown(KeyCode.Tab))
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
    }
   
    /*
    private void OnCollisionEnter(Collision collision)
    {
        // 땅에 닿았을 때
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }*/
}