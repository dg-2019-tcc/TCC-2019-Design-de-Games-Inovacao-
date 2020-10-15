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

    #region Unity Function

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

    }

    #endregion

    #region Public Functions

    public void FlowHUB()
    {
        //GameManager.Instance.LoadGame();

        flowManager.AtivaFase(PlayerPrefsManager.Instance.prefsVariables.levelIndex);
        Debug.Log(PlayerPrefsManager.Instance.prefsVariables.levelIndex);
    }

    #endregion

    #region Private Functions

    private void OfflineButtonMenu(bool isOn)
    {
        flowManager.OfflineButtonOn(isOn);
    }

    #endregion
    

}
