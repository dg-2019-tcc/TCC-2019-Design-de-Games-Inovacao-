using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveChecker : MonoBehaviour
{
	public GameObject player;
	public GameObject mc;
	public GameObject delayStartMenu;

	public float timeToSpawn;


	private void Start()
	{
		//debug
		PlayerPrefs.SetInt("hasPlayed", 0);
		if (PlayerPrefs.GetInt("hasPlayed") != 1)
		{
			StartCoroutine("Showoff");
		}
		else
		{
			mc.SetActive(false);
		}
	}



	private IEnumerator Showoff()
	{
		player.SetActive(false);
		delayStartMenu.SetActive(false);
		mc.SetActive(true);
		yield return new WaitForSeconds(timeToSpawn);
		player.SetActive(true);
		delayStartMenu.SetActive(true);
		mc.SetActive(false);
		PlayerPrefs.SetInt("hasPlayed", 1);
	}
}
