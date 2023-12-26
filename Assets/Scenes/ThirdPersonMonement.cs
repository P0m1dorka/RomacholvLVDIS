using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMonement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed = 5f;
    public float turnSMoothTime = 0.1f;
    private float turnSmoothVElocity;
    public Rigidbody rb;
    public float jumpForce = 5f;
    public float gravity;
    private Vector3 moveDir;
    private float targetAngle;
    private float angle;
    private Vector3 moveVElocity;
    
    
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1)
        {
             targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
             angle = Mathf.SmoothDamp(transform.eulerAngles.y, targetAngle, ref turnSmoothVElocity,
                turnSMoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
             moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            moveVElocity = transform.forward * speed * vertical;
        }

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            moveVElocity.y = jumpForce;
            moveVElocity.y += gravity * Time.deltaTime;
            controller.Move(moveVElocity * Time.deltaTime);
        }
        
    }
    
}
