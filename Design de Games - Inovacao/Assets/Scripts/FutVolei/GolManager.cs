﻿using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class GolManager : MonoBehaviourPunCallbacks
{
    PhotonView playerView;

    public  int index;

    public float maxPoints;

    public GameObject bola;
    public GameObject goool;

    public Transform bolaSpawnPoint;
    public FloatVariable botScore;
    public TextMeshProUGUI placarText;

    public BoolVariableArray acabou01;
    public FloatVariable flowIndex;

    public BoolVariableArray aiGanhou;
    public BoolVariable playerGanhou;

    public FeedbackText feedbackWin;



    private void Start()
    {
        acabou01 = Resources.Load<BoolVariableArray>("Acabou01");
        aiGanhou = Resources.Load<BoolVariableArray>("AIGanhou");
        playerGanhou = Resources.Load<BoolVariable>("PlayerGanhou");
        flowIndex = Resources.Load<FloatVariable>("FlowIndex");

        playerGanhou.Value = false;
        aiGanhou.Value[3] = false;
    }

    private void Update()
    {
        if (botScore.Value >= maxPoints)
        {
            feedbackWin.Perdeu();
            aiGanhou.Value[4] = true;
            playerGanhou.Value = false;
            PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 0;
            StartCoroutine("AcabouFase");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Futebol"))
        {
            bola = other.gameObject;

            GolSelect playerGol = GetComponentInParent<GolSelect>();
            playerGol.jogador.PV.Owner.AddScore(1);


            Recomeca(); 


            if (playerGol.jogador.PV.Owner.GetScore() >= 5)
            {
                feedbackWin.Ganhou();
                aiGanhou.Value[4] = false;
                playerGanhou.Value = true;
                StartCoroutine("AcabouFase");
            }
            
        }

    }

    IEnumerator AcabouFase()
    {

        yield return new WaitForSeconds(3f);
        if (aiGanhou.Value[3] == true)
        {
            if (acabou01.Value[3] == true)
            {
                LevelManager.Instance.GoPodium();
            }
            else
            {
                LevelManager.Instance.GoHub();
            }
        }

        else if (playerGanhou.Value == true)
        {
            if (acabou01.Value[3] == true)
            {
                PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 1;
                LevelManager.Instance.GoPodium();
            }
            else
            {
                LevelManager.Instance.HistFutebol();
                //flowIndex.Value = 4;
            }
        }
    }

    [PunRPC]
    public void Recomeca()
    {
        StartCoroutine("ResetaBola");
    }

    IEnumerator ResetaBola()
    {
        goool.SetActive(true);
        bola.SetActive(false);

        //bola.GetComponent<BolaFutebol>().bolaTimer += 5f;

        bola.GetComponent<Rigidbody2D>().isKinematic = true;

        bola.transform.position = bolaSpawnPoint.position;

        index++;

        yield return new WaitForSeconds(0.8f);
        placarText.text = index.ToString();
        bola.SetActive(true);

        bola.GetComponent<Rigidbody2D>().isKinematic = false;
        goool.SetActive(false);


    }
}
