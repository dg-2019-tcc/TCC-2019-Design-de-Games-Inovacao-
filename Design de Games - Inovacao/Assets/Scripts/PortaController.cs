using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaController : MonoBehaviour
{
    public int portaIndex;

    public GameObject portaObj;
    public GameObject lockObj;

    private bool doorIsOpen;

    #region Unity Function

    private void Start()
    {
        CheckDoor();
    }

    private void Update()
    {
        CheckDoor();
    }

    #endregion

    #region Public Functions

    #endregion

    #region Private Functions

    void CheckDoor()
    {
        if (CheckPointController.nextFaseIndex == portaIndex || CheckPointController.finishedGame || GameManager.historiaMode == false)
        {
            GameManager.precisaFalarTV = false;
            //if (doorIsOpen) return;
            DoorOpen(true);
        }
        else
        {
            //if (!doorIsOpen) return;
            DoorOpen(false);
        }
    }

    void DoorOpen(bool isOn)
    {
        portaObj.SetActive(isOn);
        lockObj.SetActive(!isOn);

        doorIsOpen = isOn;
    }

    #endregion

}
