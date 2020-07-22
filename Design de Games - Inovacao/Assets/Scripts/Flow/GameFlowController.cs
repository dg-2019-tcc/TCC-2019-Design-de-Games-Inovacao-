using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameFlowController : MonoBehaviour
{
    private GameFlowManager flowManager;

    public BoolVariableArray aiGanhou;
    public BoolVariable demo;

    public int levelIndex;
    public int ganhouDoKlay;
    private string sceneName;


    private void Start()
    {
        /*GameManager gameManager = GameManager.Instance;
        OfflineMode offlineMode = OfflineMode.Instance;*/

        flowManager = GetComponent<GameFlowManager>();
        sceneName = SceneManager.GetActiveScene().name;

        if (aiGanhou == null)
        {
            aiGanhou = Resources.Load<BoolVariableArray>("AIGanhou");
        }
        if (demo == null)
        {
            demo = Resources.Load<BoolVariable>("Demo");
        }

        GameManager.Instance.LoadGame();


        if (sceneName == "MenuPrincipal")
        {
            if (GameManager.levelIndex < 8 || demo.Value == true)
            {
                OfflineButtonMenu(false);
                OfflineMode.Instance.AtivaOffline(true);
            }
            else
            {
                OfflineButtonMenu(true);
            }
        }
        if(sceneName == "HUB")
        {
            if (GameManager.historiaMode)
            {
                OfflineMode.Instance.AtivaOffline(true);
                ganhouDoKlay = PlayerPrefs.GetInt("GanhouDoKlay");
                if (ganhouDoKlay == 1)
                {
                    flowManager.FechaTudo();
                }

                else
                {
                    flowManager.AtivaFase(GameManager.levelIndex);
                }
            }

            else
            {
                flowManager.AtivaFase(9);
            }
        }

    }


    public void OfflineButtonMenu(bool isOn)
    {
        flowManager.OfflineButtonOn(isOn);
    }

    public void FlowHUB()
    {
        GameManager.Instance.LoadGame();

        flowManager.AtivaFase(GameManager.levelIndex);
        Debug.Log(GameManager.levelIndex);
    }
    

}
