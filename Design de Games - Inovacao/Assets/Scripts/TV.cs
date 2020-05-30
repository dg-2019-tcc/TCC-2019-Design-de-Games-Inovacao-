using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour
{
    public bool faloComTV;


    void Start()
    {
        faloComTV = false;
    }

    void FalouComTV()
    {
        faloComTV = true;
    }
}
