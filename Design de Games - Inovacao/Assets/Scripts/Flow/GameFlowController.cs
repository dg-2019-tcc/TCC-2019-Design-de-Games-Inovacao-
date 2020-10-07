using System.Collections;
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
        OfflineMode.Instance.AtivaOffline(true);
         /*if (GameManager.sceneAtual == SceneType.MenuPrincipal)
        {
            CheckPointController.instance.LoadCheckPoint();
        }

        if (GameManager.sceneAtual == SceneType.HUB)
        {
            GameManager.historiaMode = false;
            if (GameManager.historiaMode)
            {
                OfflineMode.Instance.AtivaOffline(true);

                if(CheckPointController.nextFalaIndex != 0)
                {
                    flowManager.FechaTudo();
                }
                else
                {
                    flowManager.AtivaFase(CheckPointController.nextFaseIndex);
                }
            }

            else
            {
                flowManager.AtivaFase(8);
            }
        }*/

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
