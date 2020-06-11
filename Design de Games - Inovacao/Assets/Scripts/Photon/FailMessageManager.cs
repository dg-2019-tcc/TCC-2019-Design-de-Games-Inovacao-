using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class FailMessageManager : MonoBehaviour
{
	public GameObject message;
	public float timeToWait;


	public bool startChecking = false;
	public bool wasConnected;
	public static bool manualShutdown = false;


	private string cena;

	private HistoriaManager taNaCenaDeHist;
    // Start is called before the first frame update
    void Start()
    {
		DontDestroyOnLoad(gameObject);
    }

	void Update()
	{
		cena = SceneManager.GetActiveScene().name;

		if (cena == "MenuPrincipal" || cena == "HUB")
		{
			return;
		}


		if (!startChecking)
		{
			if (PhotonNetwork.IsConnected != wasConnected && cena != "MenuPrincipal")
			{
				startChecking = true;
			}
		}
		else
		{
			if (!PhotonNetwork.IsConnected && !manualShutdown && !message.activeSelf && cena != "HUB")
			{
				taNaCenaDeHist = FindObjectOfType<HistoriaManager>();
				if (taNaCenaDeHist == null)
				{


					manualShutdown = true;
					startChecking = false;
					StartCoroutine(WeHaveToGoBack());
				}
				taNaCenaDeHist = null;
			}
			else if (manualShutdown)
			{
				StartCoroutine(resetShutdownMode());
			}
			
		}
	}


	IEnumerator WeHaveToGoBack()
	{
		
			message.SetActive(true);

			yield return new WaitForSeconds(timeToWait);
			message.SetActive(false);
			SceneManager.LoadScene("HUB");
		

	}

	IEnumerator resetShutdownMode()
	{
		yield return new WaitForSeconds(timeToWait);
		manualShutdown = false;

	}
}
