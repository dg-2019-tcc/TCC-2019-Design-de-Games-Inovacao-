using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;


public class VoleiManager : MonoBehaviour
{
    PhotonView playerView;

    public int index;

    public float maxPoints;

    public GameObject bola;

    public Vector3 bolaSpawnPoint;
    public FloatVariable botScore;

    public TextMeshProUGUI placarText;

    public FeedbackText feedbackText;

    private BolaVolei bolaVolei;
    GolSelect playerGol;

    public Points moedas;
    public int moedasGanhas = 100;

    private void Start()
    {
        playerGol = GetComponentInParent<GolSelect>();
        bolaVolei = bola.GetComponent<BolaVolei>();
    }

    private void Update()
    {
        if (botScore.Value >= maxPoints)
        {
            feedbackText.Perdeu();
            PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 0;
            LevelManager.Instance.GoPodium();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Volei"))
        {
            bola = other.gameObject;

            index++;
            playerGol.jogador.PV.Owner.AddScore(1);

            bolaVolei.FoiPonto(bolaSpawnPoint);
            //RecomecaVolei();
            placarText.text = playerGol.jogador.PV.Owner.GetScore().ToString();

            if (playerGol.jogador.PV.Owner.GetScore() >= 7)
            {
                moedas.Add(moedasGanhas);
                feedbackText.Ganhou();
                PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 1;
                LevelManager.Instance.GoPodium();
            }

        }

    }

    IEnumerator Acabou()
    {


        yield return new WaitForSeconds(3f);
        LevelManager.Instance.GoPodium();
    }

    [PunRPC]
    public void RecomecaVolei()
    {
        StartCoroutine("ResetaBola");
    }

    IEnumerator ResetaBola()
    {
        bola.SetActive(false);

        //bola.GetComponent<BolaFutebol>().bolaTimer += 5f;

        bola.GetComponent<Rigidbody2D>().isKinematic = true;

        bola.transform.position = bolaSpawnPoint;

        index++;

        yield return new WaitForSeconds(3f);
        bola.SetActive(true);

        bola.GetComponent<Rigidbody2D>().isKinematic = false;

    }
}
