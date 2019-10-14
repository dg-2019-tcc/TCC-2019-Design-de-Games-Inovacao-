using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviourPunCallbacks
{

    public GameObject pausebuttons;

    public void Pause()
    {
        pausebuttons.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void VoltaMenu()
    {
		PhotonNetwork.Disconnect();
        
    }

    public void VoltaJogo()
    {
        pausebuttons.SetActive(false);
    }

	public override void OnDisconnected(DisconnectCause cause)
	{
		base.OnDisconnected(cause);
		SceneManager.LoadScene("DelayStartMenuDemo");
	}


}
