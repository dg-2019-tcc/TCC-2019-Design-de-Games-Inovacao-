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
    // Start is called before the first frame update
    void Start()
    {
		DontDestroyOnLoad(gameObject);
    }

	void Update()
	{

		if (!startChecking)
		{
			if (PhotonNetwork.IsConnected != wasConnected)
			{
				startChecking = true;
			}
		}
		else
		{
			if (!PhotonNetwork.IsConnected && !manualShutdown && !message.activeSelf && SceneManager.GetActiveScene().name != "HUB")
			{
				startChecking = false;
				StartCoroutine(WeHaveToGoBack());
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
