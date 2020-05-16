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
		
		ChooseScene();
		
	}

	private void ChooseScene()
	{
		switch (HistoryChecker.sceneName)
		{
			case "Coleta":
				if (PlayerPrefs.GetInt("PrimeiraVezNaColeta") == 1)
				{
					GoToHUB();
				}
				else
				{
					HistoryChecker.sceneName = null;
					PlayerPrefs.SetInt("PrimeiraVezNaColeta", 1);
					SceneManager.LoadScene("HistoriaColeta");
				}
				break;

			case "Futebol":
				if (PlayerPrefs.GetInt("PrimeiraVezNoFutebol") == 1)
				{
					GoToHUB();
				}
				else
				{
					HistoryChecker.sceneName = null;
					PlayerPrefs.SetInt("PrimeiraVezNoFutebol", 1);
					SceneManager.LoadScene("HistoriaFutebol");
				}
				break;
			
			default:
				GoToHUB();
				break;
		}
		
	}

	private void GoToHUB()
	{
		SceneManager.LoadScene("HUB");
		PhotonNetwork.Disconnect();
	}

}


