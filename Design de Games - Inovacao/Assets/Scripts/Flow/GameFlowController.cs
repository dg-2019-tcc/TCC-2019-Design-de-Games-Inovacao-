using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameFlowController : MonoBehaviour
{
    private GameFlowManager flowManager;

    public BoolVariableArray acabou01;
    public BoolVariableArray aiGanhou;
    public BoolVariable demo;

    public int levelIndex;
    private string sceneName;

    private void Start()
    {
        flowManager = GetComponent<GameFlowManager>();
        sceneName = SceneManager.GetActiveScene().name;

        if (acabou01 == null)
        {
            acabou01 = Resources.Load<BoolVariableArray>("Acabou01");
        }
        if (aiGanhou == null)
        {
            aiGanhou = Resources.Load<BoolVariableArray>("AIGanhou");
        }
        if (demo == null)
        {
            demo = Resources.Load<BoolVariable>("Demo");
        }

        FindLevelIndex();

        if(sceneName == "MenuPrincipal")
        {
            MenuFlow();
        }
        if(sceneName == "HUB")
        {
            FlowHUB();
        }

    }

    void MenuFlow()
    {
        if(levelIndex < 8 || demo.Value == true)
        {
            OfflineButtonMenu(false);
            flowManager.OnlineMode(false);
        }
        else
        {
            OfflineButtonMenu(true);
        }
    }

    public void OfflineButtonMenu(bool isOn)
    {
        flowManager.OfflineButtonOn(isOn);
    }

    public void FlowHUB()
    {
        FindLevelIndex();
        flowManager.AtivaFase(levelIndex);
        Debug.Log(levelIndex);
    }
    
    void FindLevelIndex()
    {
        for (int i = 0; i < acabou01.Value.Length; i++)
        {
            if (!acabou01.Value[i])
            {
                levelIndex = i;

                break;
            }
        }
    }
}
