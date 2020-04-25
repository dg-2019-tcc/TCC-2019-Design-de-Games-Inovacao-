using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SendBackHUBTimer : MonoBehaviour
{
	public float tempoPraVoltar;
    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(TimerToGo(tempoPraVoltar));
    }

    // Update is called once per frame
    void Update()
    {
		
	}

	IEnumerator TimerToGo(float tempo)
	{

		yield return new WaitForSeconds(tempo);
		PhotonNetwork.Disconnect();
		SceneManager.LoadScene("HUB");
	}
}
