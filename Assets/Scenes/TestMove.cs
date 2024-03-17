using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 50f;
    public float rotationSpeed = 100f;

    private Rigidbody rb;
    private bool isGrounded;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    Vector2 inputVector;
    Vector3 moveVector;

    void Update()
    {

        float rotateAmount = moveVector.x * rotationSpeed;
        transform.Rotate(new Vector3(0f, 0f + rotateAmount, 0f));


        // �̵� ó��
        transform.Translate(moveVector.normalized * Time.deltaTime * moveSpeed);
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        inputVector = value.ReadValue<Vector2>();

        // �Է� ���͸� �̵� ���ͷ� ��ȯ
        moveVector = new Vector3(inputVector.x, moveVector.y, inputVector.y).normalized;
        if (inputVector.y != 0)
        {
            animator.SetFloat("isWalk", inputVector.y);
            print("����");
        }

    }
    private void LateUpdate()
    {

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