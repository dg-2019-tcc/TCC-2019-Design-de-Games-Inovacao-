using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairManager : MonoBehaviour
{
    public GameObject[] hairModel;



    public void Start()
    {

        int index = PlayerPrefs.GetInt("hairIndex");

        hairModel[index].SetActive(true);

    }
}
