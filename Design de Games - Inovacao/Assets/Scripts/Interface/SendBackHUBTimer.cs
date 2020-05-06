using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SendBackHUBTimer : MonoBehaviour
{
	public float tempoPraVoltar;

    void Start()
    {
		StartCoroutine(TimerToGo(tempoPraVoltar));
    }

   

	IEnumerator TimerToGo(float tempo)
	{

		yield return new WaitForSeconds(tempo);
		PhotonNetwork.Disconnect();
		ChooseScene();
		
	}

	private void ChooseScene()
	{
		switch (HistoryChecker.sceneName)
		{
			case "Coleta":
				if (PlayerPrefs.GetInt("PrimeiraVezNaColeta") == 1)
				{
					PlayerPrefs.SetInt("PrimeiraVezNaColeta", 1);
					SceneManager.LoadScene("HUB");
				}
				else
				{
					HistoryChecker.sceneName = null;
					SceneManager.LoadScene("HistoriaColeta");
				}
				break;

			case "Futebol":
				if (PlayerPrefs.GetInt("PrimeiraVezNoFutebol") == 1)
				{
					PlayerPrefs.SetInt("PrimeiraVezNoFutebol", 1);
					SceneManager.LoadScene("HUB");
				}
				else
				{
					HistoryChecker.sceneName = null;
					SceneManager.LoadScene("HistoriaFutebol");
				}
				break;
			
			default:
				SceneManager.LoadScene("HUB");
				break;
		}
		
	}

}
