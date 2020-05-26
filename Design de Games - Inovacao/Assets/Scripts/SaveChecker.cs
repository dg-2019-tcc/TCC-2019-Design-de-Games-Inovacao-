using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveChecker : MonoBehaviour
{
	public GameObject player;
	public GameObject mc;
	public GameObject delayStartMenu;

	public float timeToSpawn;

    public FloatVariable flowIndex;

	private void Start()
	{

        flowIndex = Resources.Load<FloatVariable>("FlowIndex");

        if (flowIndex.Value == 1)
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
		PlayerPrefs.SetInt("hasPlayed", 1);
        mc.SetActive(false);
	}
}
