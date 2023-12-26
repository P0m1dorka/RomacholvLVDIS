using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMonement : MonoBehaviour
{
    public Transform cameraTransform;
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float jumpForce = 5f;
    
    private CharacterController characterController;
    private Vector3 moveDirection;
    private bool isJumping;
    private float verticalVelocity;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Перемещение игрока по направлению камеры
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f;
        right.y = 0f;
        moveDirection = (forward * verticalInput + right * horizontalInput).normalized;

        // Прыжок
        if (characterController.isGrounded)
        {
            isJumping = false;
            verticalVelocity = -0.5f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
                isJumping = true;
            }
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        // Поворот игрока
        float rotationInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * rotationInput * rotationSpeed * Time.deltaTime);

        // Применение перемещения и гравитации
        moveDirection.y = verticalVelocity;
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}
