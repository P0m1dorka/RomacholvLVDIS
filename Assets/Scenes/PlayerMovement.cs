using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Camera _camera;
    [SerializeField] private float speed;

    [SerializeField] private TMP_Text _text;
    [SerializeField] private int pickObj;
    [SerializeField] private int max;
    [SerializeField] private float jumpSpeed;
    private float vertical;
    private Vector3 screenMousePostion;
    private Vector3 worldMousePos;
    private float horizonata;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = _camera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, _camera.transform.position.y - transform.position.y));

        Vector3 lookAtPosition = new Vector3(mousePosition.x, transform.position.y, mousePosition.z);
        transform.LookAt(lookAtPosition);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAtPosition - transform.position), 0.3f * Time.deltaTime);
       // screenMousePostion = Input.mousePosition;
       // worldMousePos = _camera.ScreenToWorldPoint(screenMousePostion);
        //gameObject.transform.LookAt(worldMousePos);
        vertical = Input.GetAxis("Vertical");
        horizonata = Input.GetAxis("Horizontal");
        rb.position += (new Vector3(horizonata, 0f, vertical) * speed * Time.deltaTime);
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.AddForce(0,jumpSpeed,0,ForceMode.Impulse);
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
                    Debug.Log("end");
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
