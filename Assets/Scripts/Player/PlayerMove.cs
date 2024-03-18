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
    public float            moveSpeed = 5f;
    public float            jumpForce = 50f;
    public float            rotationSpeed = 100f;
    public bool             isBrokenAttack = false;
    public SPYTargetObject  spy_Target_Object = null;

    private Rigidbody       rb;
    private WeaponControll  weaponControll;
    private SPYAction       spyAction;
    private Vector2         inputVector;
    private Vector3         moveVector;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        weaponControll = GetComponent<WeaponControll>();
        spyAction = GetComponent<SPYAction>();
    }
    void Update()
    {
        float rotateAmount = moveVector.x * rotationSpeed;
        transform.Rotate(new Vector3( 0f, 0f + rotateAmount, 0f) ) ;
        // 이동 처리
        transform.Translate( moveVector.normalized * Time.deltaTime * moveSpeed);
        // 인풋매니저로 수정 해야 할듯 함.
        if (Input.GetKey(KeyCode.Alpha1))
        {
            weaponControll.WeaponChange(0);
            weaponControll.weaponState = WeaponControll.WeaponState.none;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            weaponControll.WeaponChange(1);
            weaponControll.weaponState = WeaponControll.WeaponState.equip;
        }
        if(Input.GetKey(KeyCode.Alpha3))
        {
            weaponControll.weaponState = WeaponControll.WeaponState.skill;
            weaponControll.WeaponChange(2);
        }
        if (Input.GetKey(KeyCode.F)&& isBrokenAttack)
        {
            spyAction.ExposedAction(10f);
            // TODO spyAction.BrokenObjectAttack(spy_Target_Object);
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