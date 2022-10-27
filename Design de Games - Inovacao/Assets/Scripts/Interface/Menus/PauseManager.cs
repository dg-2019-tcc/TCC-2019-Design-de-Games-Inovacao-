using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityCore.Scene;

public class PauseManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private string nomeDoMenu;

    public GameObject pausebuttons;

	private bool goBack;

    public Points moedas;

    #region Unity Function

    private void Start()
    {
        goBack = false;
    }

    #endregion

    #region Public Functions

    public void Pause()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
        pausebuttons.SetActive(true);
        moedas.JustShowCoins();
        //GarbageController.EnableGC();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void VoltaMenu()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
        FailMessageManager.manualShutdown = true;
        goBack = true;
        PhotonNetwork.Disconnect();

    }

    public void VoltaJogo()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
        pausebuttons.SetActive(false);
        //GarbageController.DisableGC();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        if (goBack) { LoadingManager.instance.LoadNewScene(SceneType.MenuPrincipal, GameManager.sceneAtual, false); }
        //SceneManager.LoadScene(nomeDoMenu);
    }

    #endregion

    #region Private Functions

    #endregion
}
