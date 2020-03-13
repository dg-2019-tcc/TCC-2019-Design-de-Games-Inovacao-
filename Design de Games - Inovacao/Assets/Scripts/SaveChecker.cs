using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveChecker : MonoBehaviour
{
	public GameObject player;
	public GameObject delayStartMenu;


	private void Start()
	{
		//debug
		//PlayerPrefs.SetInt("hasPlayed", 0);
		if (PlayerPrefs.GetInt("hasPlayed") != 1){
			player.SetActive(false);
			delayStartMenu.SetActive(false);
			StartCoroutine("Showoff");
		}
	}



	private IEnumerator Showoff()
	{
		yield return new WaitForSeconds(10);
		player.SetActive(true);
		delayStartMenu.SetActive(true);
		PlayerPrefs.SetInt("hasPlayed", 1);
	}
}
