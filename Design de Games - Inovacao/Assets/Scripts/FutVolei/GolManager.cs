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

    public PhotonView bolaPV;

    public BolaFutebol bolaFutebol;

	GolSelect playerGol;


	private bool isLoading = false;

    private bool offline;

    public static bool desativaBola;
    public static bool ativaBola;

	[Header("Variáveis das Moedas")]
	public Points moedas;
	public int moedasGanhasNessaFase = 100;
    private bool needAddCoins;

    #region Unity Function

    private void Start()
    {
        acabou01 = Resources.Load<BoolVariableArray>("Acabou01");
        aiGanhou = Resources.Load<BoolVariableArray>("AIGanhou");
        playerGanhou = Resources.Load<BoolVariable>("PlayerGanhou");
        flowIndex = Resources.Load<FloatVariable>("FlowIndex");
        botScore = Resources.Load<FloatVariable>("BotScore");

        botScore.Value = 0;

        needAddCoins = true;

        playerGanhou.Value = false;
        aiGanhou.Value[4] = false;

        offline = OfflineMode.modoDoOffline;

        playerGol = GetComponentInParent<GolSelect>();
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
                //StartCoroutine("AcabouFase");
                //Acaba();
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
                //moedas.Add(moedasGanhasNessaFase);
                feedbackWin.Ganhou();
                //aiGanhou.Value[4] = false;
                playerGanhou.Value = true;
                LevelManager.Instance.Ganhou();
                if (needAddCoins == true)
                {
                    moedas.Add(moedasGanhasNessaFase);
                }
                needAddCoins = false;
                //PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 1;
                //if (isLoading) return;
                //isLoading = true;
                //StartCoroutine("AcabouFase");
                //Acaba();
            }

        }

    }

    #endregion

    #region Public Functions

    public void PerdeuProBot()
    {
        Debug.Log("Perdeu");
        feedbackWin.Perdeu();
        aiGanhou.Value[4] = true;
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
