﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortaTutorial : MonoBehaviour
{
    public Joystick joy;

    private float joyGambiarra;
    public bool abriPorta;

    public BoolVariable buildPC;
    private Vector2 keyInput;

    // Start is called before the first frame update 
    void Start()
    {
        joy = FindObjectOfType<Joystick>();
        buildPC = Resources.Load<BoolVariable>("BuildPC");
    }

    // Update is called once per frame 
    void Update()
    {
        if (buildPC.Value == false)
        {
            if (joy == null)
            {
                joy = FindObjectOfType<Joystick>();
            }

            if (joyGambiarra < joy.Vertical)
            {
                if (joy.Vertical >= 0.8f && abriPorta)
                {
                    //SceneManager.LoadScene("HUB"); 
                    Debug.Log("Colidiu");
                    OpenDoorTutorial();
                }
            }

            joyGambiarra = joy.Vertical;
        }
        else
        {
            keyInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if(keyInput.y > 0 && abriPorta)
            {
                OpenDoorTutorial();
            }
        }

    }

    public void OpenDoorTutorial()
    {

        SceneManager.LoadScene("HUB");

       // FindObjectOfType<PauseManager>().VoltaMenu();
    }
}

