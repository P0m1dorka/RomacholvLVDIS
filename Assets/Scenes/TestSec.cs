using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class TestSec : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private int pickObj;
    [SerializeField] private int max;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zone"))
        {
            other.gameObject.GetComponent<BlockScript>().PowerOn();
        }

        if (other.gameObject.CompareTag("TriggeLva"))
        {
            SceneManager.LoadScene("LEVEL");
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
                _text.text = $"Picked {pickObj}";
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
