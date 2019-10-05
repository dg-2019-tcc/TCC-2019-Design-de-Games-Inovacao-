using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    public GameObject[] shirtModel;


    public void Start()
    {

        int index = PlayerPrefs.GetInt("chestIndex");

        shirtModel[index].SetActive(true);

    }
}
