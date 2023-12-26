using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMonement : MonoBehaviour
{
    public CharacterController characterController;
    public Transform cameraTransform;
    public float movementSpeed = 5f;
    public float rotationSpeed = 5f;
    public float jumpForce = 5f;
    public float gravity = 20f;
    private Vector3 moveDirection;
    private bool isJumping = false;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        bool jumpInput = Input.GetKeyDown(KeyCode.Space);
        if (characterController.isGrounded)
        {
            isJumping = false;
            moveDirection = new Vector3(horizontalInput, 0f, verticalInput);
            moveDirection = cameraTransform.TransformDirection(moveDirection);
            moveDirection *= movementSpeed;
            Quaternion rot = Quaternion.LookRotation(cameraTransform.forward, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);
            moveDirection.y = 0f;
            if (jumpInput)
            {
                moveDirection.y = jumpForce;
                isJumping = true;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
