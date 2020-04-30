using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class FailMessageManager : MonoBehaviour
{
	public GameObject message;
	public float timeToWait;
    // Start is called before the first frame update
    void Start()
    {
		DontDestroyOnLoad(gameObject);
    }

	// Update is called once per frame
	void OnPhotonPlayerDisconnected()
	{
		StartCoroutine(WeHaveToGoBack());
	}


	IEnumerator WeHaveToGoBack()
	{
		if (SceneManager.GetActiveScene().name != "HUB")
		{
			message.SetActive(true);

			yield return new WaitForSeconds(timeToWait);
			message.SetActive(false);
			SceneManager.LoadScene("HUB");
		}
		else yield return new WaitForSeconds(0);

	}
}
