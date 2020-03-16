using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveChecker : MonoBehaviour
{
	public GameObject player;
	public GameObject delayStartMenu;

	public float timeToSpawn;

    public GameObject mc;


	private void Start()
	{
		//debug
		PlayerPrefs.SetInt("hasPlayed", 0);
		if (PlayerPrefs.GetInt("hasPlayed") != 1){
			player.SetActive(false);
			delayStartMenu.SetActive(false);
			StartCoroutine("Showoff");
		}
	}



	private IEnumerator Showoff()
	{
		yield return new WaitForSeconds(timeToSpawn);
		player.SetActive(true);
		delayStartMenu.SetActive(true);
		PlayerPrefs.SetInt("hasPlayed", 1);
        mc.SetActive(false);
	}
}
