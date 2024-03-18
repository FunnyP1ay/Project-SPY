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
    private WeaponControl  weaponControl;
    private SPYAction       spyAction;
    private Vector2         inputVector;
    private Vector3         moveVector;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        weaponControl = GetComponent<WeaponControl>();
        spyAction = GetComponent<SPYAction>();
    }
    void Update()
    {
        float rotateAmount = moveVector.x * rotationSpeed;
        transform.Rotate(new Vector3( 0f, 0f + rotateAmount, 0f) ) ;
        // �̵� ó��
        transform.Translate( moveVector.normalized * Time.deltaTime * moveSpeed);
        // ��ǲ�Ŵ����� ���� �ؾ� �ҵ� ��.
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
        if (Input.GetKey(KeyCode.F)&& isBrokenAttack)
        {
            spyAction.ExposedAction(10f);
            // TODO spyAction.BrokenObjectAttack(spy_Target_Object);
        }
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        inputVector = value.ReadValue<Vector2>();

        // �Է� ���͸� �̵� ���ͷ� ��ȯ
        moveVector = new Vector3(inputVector.x, moveVector.y , inputVector.y);
    }
   
    /*
    private void OnCollisionEnter(Collision collision)
    {
        // ���� ����� ��
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }*/
}