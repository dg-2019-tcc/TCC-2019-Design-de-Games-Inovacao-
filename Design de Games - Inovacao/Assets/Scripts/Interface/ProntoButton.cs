﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.SceneManagement;
using UnityCore.Scene;

public class ProntoButton : MonoBehaviour
{
    [SerializeField]
    private string nomeDoMenu;
    [SerializeField]
    private string tutorial;
    [SerializeField]
    private string hub;

    public BoolVariable jaJogou;

    public SceneType nextScene;

    #region Unity Function

    #endregion

    #region Public Functions

    public void ComecaJogo()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Start", GetComponent<Transform>().position);

        CheckPointController.instance.WonGameCheckPoint();
        LoadingManager.instance.LoadNewScene(nextScene, GameManager.sceneAtual, false);
    }

    public void TutorialStart()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Start", GetComponent<Transform>().position);
        DelayStartWaitingRoomController.minPlayerToStart = 1;
        DelayStartWaitingRoomController.tutorialMode = true;
        CreateTutorialRoom();
    }

    #endregion

    #region Private Functions

    void ChamaTutorial()
    {
        if (PlayerPrefs.HasKey("NickName"))
        {
            if (PlayerPrefs.GetString("NickName") == "")
            {
                PhotonNetwork.NickName = "Player " + Random.Range(0, 1000);
            }
            else
            {
                PhotonNetwork.NickName = PlayerPrefs.GetString("NickName");
            }
        }
        else
        {
            PhotonNetwork.NickName = "Player " + Random.Range(0, 1000);
        }
    }


    void CreateTutorialRoom()
    {
        int randomRoomNumber = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = false, IsOpen = false, MaxPlayers = (byte)1 };
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps);
    }

    #endregion

}
