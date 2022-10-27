﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;


public class FinalizaTutorial : MonoBehaviour
{
    [Header("Tela de vitória")]
    [SerializeField]
    private string victoryLevel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //    PhotonNetwork.LoadLevel(victoryLevel);
            /*PhotonNetwork.Disconnect();
			SceneManager.LoadScene(victoryLevel);*/
            if (PhotonNetwork.OfflineMode == true)
            {
                SceneManager.LoadScene("HUB");
            }
            FindObjectOfType<PauseManager>().VoltaMenu();

        }
    }

    public void OpenDoor()
    {
        if (PhotonNetwork.OfflineMode == true)
        {
            SceneManager.LoadScene("HUB");
        }
        FindObjectOfType<PauseManager>().VoltaMenu();
    }
}
