using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsManager : MonoBehaviour
{
    public GameObject[] pantsModel;


    public void Start()
    {

        int index = PlayerPrefs.GetInt("legsIndex");

        pantsModel[index].SetActive(true);

    }
}
