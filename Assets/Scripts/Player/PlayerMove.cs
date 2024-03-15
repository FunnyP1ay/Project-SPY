using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 50f;
    public float rotationSpeed = 30f;

    private Rigidbody rb;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    Vector2 inputVector;
    Vector3 moveVector;

    void Update()
    {


        transform.Rotate(new Vector3( 0f, 0f + moveVector.x, 0f) * Time.deltaTime) ;


        // �̵� ó��
        transform.Translate( moveVector.normalized * Time.deltaTime * moveSpeed);
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        inputVector = value.ReadValue<Vector2>();

        // �Է� ���͸� �̵� ���ͷ� ��ȯ
        moveVector = new Vector3(inputVector.x, moveVector.y , inputVector.y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ���� ����� ��
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}