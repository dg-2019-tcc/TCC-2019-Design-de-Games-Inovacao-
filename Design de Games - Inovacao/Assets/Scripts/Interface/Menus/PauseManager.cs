using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private string nomeDoMenu;

    public GameObject pausebuttons;

	private bool goBack;

	private void Start()
	{
		goBack = false;
	}

	public void Pause()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
        pausebuttons.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void VoltaMenu()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
		FailMessageManager.manualShutdown= true;
        PhotonNetwork.Disconnect();
		goBack = true;
    }

    public void VoltaJogo()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
        pausebuttons.SetActive(false);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        if (goBack)
            SceneManager.LoadScene(nomeDoMenu);
    }
}
