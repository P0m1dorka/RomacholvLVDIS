using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField] private float jumpspeed;
    [SerializeField] private bool onJump;

    private Rigidbody rb;
    private Vector3 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // ѕолучаем ввод по горизонтали и вертикали (направление движени€)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // —оздаем вектор движени€, учитыва€ направление взгл€да игрока
        movement = new Vector3(moveHorizontal, 0f, moveVertical).normalized;

        // ѕоворачиваем игрока в направлении его взгл€да
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }
        if (Input.GetKeyUp(KeyCode.Space) && !onJump)
        {
            rb.AddForce(Vector3.up * jumpspeed, ForceMode.Impulse);
        }
        
    }

    void FixedUpdate()
    {
        // ѕримен€ем силу дл€ передвижени€ игрока
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        onJump = false  ;
    }
    private void OnCollisionExit(Collision collision)
    {
        onJump = true;
    }
}
