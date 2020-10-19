using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class GolManager : MonoBehaviourPunCallbacks
{
    public GameObject bola;
    public GameObject goool;
    public FloatVariable botScore;
    public BoolVariable playerGanhou;
    public FeedbackText feedbackWin;
    public PhotonView bolaPV;
    public BolaFutebol bolaFutebol;
	GolSelect playerGol;
	private bool isLoading = false;

	[Header("Variáveis das Moedas")]
	public Points moedas;
	public int moedasGanhasNessaFase = 100;
    private bool needAddCoins;

    #region Unity Function

    private void Start()
    {
        playerGanhou = Resources.Load<BoolVariable>("PlayerGanhou");
        botScore = Resources.Load<FloatVariable>("BotScore");
        playerGol = GetComponentInParent<GolSelect>();

        botScore.Value = 0;
        needAddCoins = true;
        playerGanhou.Value = false;

        bolaPV = bola.GetPhotonView();
    }

    private void Update()
    {
        foreach (Photon.Realtime.Player p in PhotonNetwork.PlayerList)
        {
            if (p.GetScore() >= 5)
            {
                if (isLoading) return;
                isLoading = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Futebol"))
        {
            playerGol.jogador.PV.Owner.AddScore(1);

            bolaFutebol.FoiGol();

            if (playerGol.jogador.PV.Owner.GetScore() >= 5)
            {
                feedbackWin.Ganhou();
                playerGanhou.Value = true;
                LevelManager.Instance.Ganhou();
                if (needAddCoins == true)
                {
                    moedas.Add(moedasGanhasNessaFase);
                }
                needAddCoins = false;
            }

        }

    }

    #endregion

    #region Public Functions

    public void PerdeuProBot()
    {
        Debug.Log("Perdeu");
        feedbackWin.Perdeu();
        playerGanhou.Value = false;
        PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 0;
        if (isLoading) return;
        isLoading = true;
        LevelManager.Instance.Perdeu();
    }

    #endregion

    #region Private Functions

    #endregion

}
