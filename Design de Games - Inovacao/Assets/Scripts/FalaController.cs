using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalaController : MonoBehaviour
{
    public GameObject[] falaObj;

    private void Start()
    {
        Debug.Log(CheckPointController.nextFaseIndex);
        if (CheckPointController.nextFaseIndex == 0 && GameManager.historiaMode == true)
        {
            Debug.Log(CheckPointController.nextFalaIndex);
            falaObj[CheckPointController.nextFalaIndex-1].SetActive(true);
            if(CheckPointController.nextFalaIndex == 1)
            {
                falaObj[1].SetActive(true);
            }
        }
    }
}
