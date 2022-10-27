﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalaController : MonoBehaviour
{
    public GameObject[] falaObj;

    #region Unity Function

    private void Start()
    {
        Debug.Log(CheckPointController.nextFaseIndex);
        if (CheckPointController.nextFaseIndex == 0 && GameManager.historiaMode == true)
        {
            GameManager.precisaFalarTV = true;
            Debug.Log(CheckPointController.nextFalaIndex);
            falaObj[CheckPointController.nextFalaIndex - 1].SetActive(true);
            if (CheckPointController.nextFalaIndex == 1)
            {
                falaObj[1].SetActive(true);
            }
        }
    }

    #endregion

    #region Public Functions

    #endregion

    #region Private Functions

    #endregion
}
