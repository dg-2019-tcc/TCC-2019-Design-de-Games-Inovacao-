using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityCore.Scene;

public class GameFlowController : MonoBehaviour
{
    private GameFlowManager flowManager;
    public BoolVariable demo;

    public int ganhouDoKlay;

    #region Unity Function

    private void Start()
    {
        flowManager = GetComponent<GameFlowManager>();
        if (demo == null){ demo = Resources.Load<BoolVariable>("Demo");}
        OfflineMode.Instance.AtivaOffline(true);
    }

    #endregion

    #region Public Functions

    public void FlowHUB()
    {
        //flowManager.AtivaFase(PlayerPrefsManager.Instance.prefsVariables.levelIndex);
        Debug.Log(PlayerPrefsManager.Instance.prefsVariables.levelIndex);
    }

    #endregion

    #region Private Functions

    #endregion
    

}
