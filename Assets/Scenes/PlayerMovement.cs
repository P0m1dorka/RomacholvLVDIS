using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Sequences;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Camera _camera;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private int pickObj;
    [SerializeField] private int max;
    public float movementSpeed = 5f;
    //public float rotationSpeed = 100f;
    public float jumpForce = 5f;
    
   // private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Перемещение
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical) * movementSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);

        // Поворот
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            Vector3 newDirection = new Vector3(moveHorizontal, 0f, moveVertical);
            Quaternion toRotation = Quaternion.LookRotation(newDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        // Прыжок
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("TriggeLva"))
        {
            SceneManager.LoadScene("LEVEL");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zone"))
        {
            other.gameObject.GetComponent<BlockScript>().PowerOn();
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Zone"))
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                Debug.Log("PICKUP");
                pickObj++;
                _text.text = $"Picked{pickObj}";
                Destroy(other.gameObject);
                if (pickObj >= max)
                {
                    SceneManager.LoadScene("EndScene");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Zone"))
        {
            other.gameObject.GetComponent<BlockScript>().PowerOff();
        }
    }
}
