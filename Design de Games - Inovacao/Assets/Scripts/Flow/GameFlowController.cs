﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityCore.Scene;

public class GameFlowController : MonoBehaviour
{
    private GameFlowManager flowManager;

    public BoolVariableArray aiGanhou;
    public BoolVariable demo;

    public int levelIndex;
    public int ganhouDoKlay;
    //private string sceneName;


    private void Start()
    {
        flowManager = GetComponent<GameFlowManager>();
        //sceneName = SceneManager.GetActiveScene().name;
        //sceneName = SceneManager.GetSceneAt();

        if (aiGanhou == null)
        {
            aiGanhou = Resources.Load<BoolVariableArray>("AIGanhou");
        }
        if (demo == null)
        {
            demo = Resources.Load<BoolVariable>("Demo");
        }

        //GameManager.Instance.LoadGame();

        //Debug.Log(sceneName);
        //if (sceneName == "MenuPrincipal")
        if(GameManager.sceneAtual == SceneType.MenuPrincipal)
        {
            if (PlayerPrefsManager.Instance.prefsVariables.levelIndex < 8 || demo.Value == true)
            {
                OfflineButtonMenu(false);
                OfflineMode.Instance.AtivaOffline(true);
            }
            else
            {
                OfflineButtonMenu(true);
            }
        }
        //if(sceneName == "HUB")
        if (GameManager.sceneAtual == SceneType.HUB)
        {
            if (GameManager.historiaMode)
            {
                OfflineMode.Instance.AtivaOffline(true);
                //ganhouDoKlay = PlayerPrefs.GetInt("GanhouDoKlay");
                if (PlayerPrefsManager.Instance.prefsVariables.levelIndex > 1 && PlayerPrefsManager.Instance.prefsVariables.levelIndex < 8)
                {
                    if (PlayerPrefsManager.Instance.prefsVariables.levelIndex == 3 || PlayerPrefsManager.Instance.prefsVariables.levelIndex == 4)
                    {
                        if (PlayerPrefsManager.Instance.prefsVariables.levelIndex == PlayerPrefsManager.Instance.prefsVariables.falasIndex)
                        {
                            flowManager.FechaTudo();
                        }
                        else
                        {
                            flowManager.AtivaFase(PlayerPrefsManager.Instance.prefsVariables.levelIndex);
                        }
                    }
                }
                else
                {
                    if (PlayerPrefsManager.Instance.prefsVariables.levelIndex == 0)
                    {
                        flowManager.FechaTudo();
                    }

                }
            }

            else
            {
                flowManager.AtivaFase(8);
            }
        }

    }


    public void OfflineButtonMenu(bool isOn)
    {
        flowManager.OfflineButtonOn(isOn);
    }

    public void FlowHUB()
    {
        //GameManager.Instance.LoadGame();

        flowManager.AtivaFase(PlayerPrefsManager.Instance.prefsVariables.levelIndex);
        Debug.Log(PlayerPrefsManager.Instance.prefsVariables.levelIndex);
    }
    

}