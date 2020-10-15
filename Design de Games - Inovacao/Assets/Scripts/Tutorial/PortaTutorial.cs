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

    #region Unity Function

    void Start()
    {
        joy = FindObjectOfType<Joystick>();
        buildPC = Resources.Load<BoolVariable>("BuildPC");
    }

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

            if (keyInput.y > 0 && abriPorta)
            {
                OpenDoorTutorial();
            }
        }

    }

    #endregion

    #region Public Functions

    public void OpenDoorTutorial()
    {
        PlayerPrefsManager.Instance.SavePlayerPrefs("LevelIndex", 1);

        CheckPointController.instance.TalkCheckPoint(1);

        LoadingManager.instance.LoadNewScene(UnityCore.Scene.SceneType.HUB, UnityCore.Scene.SceneType.Tutorial2, false);
    }

    #endregion

    #region Private Functions

    #endregion
}

