using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaController : MonoBehaviour
{
    public int portaIndex;

    public GameObject portaObj;
    public GameObject lockObj;

    private bool doorIsOpen;

    private void Start()
    {
        CheckDoor();
    }

    private void Update()
    {
        CheckDoor();
    }


    void CheckDoor()
    {
        if (CheckPointController.nextFaseIndex == portaIndex || CheckPointController.finishedGame || GameManager.historiaMode == false)
        {
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
}
