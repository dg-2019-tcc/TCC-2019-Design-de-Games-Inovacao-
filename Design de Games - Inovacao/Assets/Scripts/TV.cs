using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour
{
    public bool faloComTV;
    public bool precisaFalar;

    private ItemLocatorOnScreen pointer;

    public BoolVariable acabou01;

    public GameObject falas;


    void Start()
    {
        pointer = GetComponent<ItemLocatorOnScreen>();

        if (acabou01 == null)
        {
            acabou01 = Resources.Load<BoolVariable>("Acabou01");
        }

        if(acabou01.Value == true)
        {
            falas.SetActive(false);
            pointer.enabled = false;
            precisaFalar = false;
        }
        else
        {
            falas.SetActive(true);
            pointer.enabled = true;
            precisaFalar = true;
        }

        faloComTV = false;
    }

    void FalouComTV()
    {
        pointer.enabled = false;
        precisaFalar = false;
        faloComTV = true;
    }
}
