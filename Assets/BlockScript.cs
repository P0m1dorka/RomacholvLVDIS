using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockScript : MonoBehaviour
{

    [SerializeField] private GameObject picture;
    // Start is called before the first frame update
    void Start()
    {
        PowerOff();
    }

    public void PowerOn()
    {
        picture.SetActive(true);
    }
    public void PowerOff()
    {
        picture.SetActive(false);
    }

}
