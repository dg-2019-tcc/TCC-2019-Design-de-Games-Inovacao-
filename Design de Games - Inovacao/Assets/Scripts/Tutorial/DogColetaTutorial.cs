﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DogColetaTutorial : MonoBehaviour
{
	//	public GameSetupController gsp;
		public BoolVariable dogSpawn;

	private PhotonView pv;
	public GameObject coletavel;

	public bool dog;
	private bool araki = true;
    // Start is called before the first frame update
    void Start()
    {
		// dogSpawn = gsp.PlayerInst.GetComponent<PhotonPlayer>().myAvatar.GetComponent<DogController>().Pet;
		//		dogSpawn.Value = false;
		if (dog)
		{
			pv = GetComponent<PhotonView>();
		
			dogSpawn.Value = false;
		}
		coletavel.SetActive(false);
    }

	// Update is called once per frame
	 void Update()
	 {
		if (dog && araki && dogSpawn.Value)
		{
			dogSpawn.Value = false;
			//dog = false;
			araki = false;
		}
	 }


	private void OnTriggerEnter2D(Collider2D collision)
	{
		//		dogSpawn.Value = true;
		if (collision.CompareTag("Player"))
		{
			if (dog)
			{
				dogSpawn.Value = true;
			}
			coletavel.SetActive(true);
			Destroy(gameObject);
		}
	}
}
