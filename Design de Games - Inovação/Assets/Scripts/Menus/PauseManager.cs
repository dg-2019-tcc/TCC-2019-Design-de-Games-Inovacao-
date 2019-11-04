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
        pausebuttons.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void VoltaMenu()
    {
		PhotonNetwork.Disconnect();
		goBack = true;
        
    }

    public void VoltaJogo()
    {
        pausebuttons.SetActive(false);
    }

	public override void OnDisconnected(DisconnectCause cause)
	{
		base.OnDisconnected(cause);
		if(goBack)
			SceneManager.LoadScene(nomeDoMenu);
	}


}
