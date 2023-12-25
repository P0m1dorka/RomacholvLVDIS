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
        // �������� ���� �� ����������� � ��������� (����������� ��������)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // ������� ������ ��������, �������� ����������� ������� ������
        movement = new Vector3(moveHorizontal, 0f, moveVertical).normalized;

        // ������������ ������ � ����������� ��� �������
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
        // ��������� ���� ��� ������������ ������
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
