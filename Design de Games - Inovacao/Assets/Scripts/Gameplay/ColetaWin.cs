using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
public class ColetaWin : MonoBehaviour
{
	private Player[] players;
	private int[] score;
	private int compareScore;
	private Player winning;

	private ColetavelGerador coletavelGerador;

	public int totalColetaveis;


	private bool ganhouJa;

    public BoolVariableArray acabou01;
    public FloatVariable flowIndex;
    public FloatVariable botScore;

    private string faseNome;

    private bool acabouTutorial;
    private int indexTutorial;

	private bool isEmpatado;

    public BoolVariableArray aiGanhou;
    public BoolVariable playerGanhou;

    public FeedbackText feedbackWin;


	[Header("Variáveis das Moedas")]
	public Points moedas;
	public int moedasGanhasNessaFase;

    private bool finished;

    #region Unity Function

    private void Start()
    {
        coletavelGerador = FindObjectOfType<ColetavelGerador>();
        aiGanhou = Resources.Load<BoolVariableArray>("AIGanhou");
        playerGanhou = Resources.Load<BoolVariable>("PlayerGanhou");

        aiGanhou.Value[1] = false;
        playerGanhou.Value = false;

        winning = PhotonNetwork.PlayerList[0];
        ganhouJa = false;
        isEmpatado = false;
    }


    void Update()
    {
        if (finished) return;
        if (botScore.Value > 4)
        {
            LevelManager.Instance.Perdeu();
            finished = true;
        }

        foreach (Photon.Realtime.Player p in PhotonNetwork.PlayerList)
        {
            //Debug.Log(p.ActorNumber);

            //players[p.ActorNumber] = p;
            //score[p.ActorNumber] = p.GetScore();
            if (compareScore - p.GetScore() < 0)
            {
                compareScore = p.GetScore();
                winning = p;
                isEmpatado = false;
            }
            else if (winning != p && compareScore == p.GetScore())
            {
                isEmpatado = true;
            }
        }


        if (coletavelGerador.coletaveis.Length <= 0)
        {
            if (isEmpatado)
            {
                coletavelGerador.DrawProtocol();
                return;
            }
            if (ganhouJa) return;
            if (OfflineMode.modoDoOffline && compareScore < 4)
            {
                feedbackWin.Perdeu();

                LevelManager.Instance.Perdeu();
                finished = true;

            }
            else
            {
                if (PhotonNetwork.LocalPlayer == winning)
                {
                    LevelManager.Instance.Ganhou();
                    Debug.Log("Ganhou");
                    feedbackWin.Ganhou();
                    winning.CustomProperties["Ganhador"] = 1;
                }
                else
                {
                    LevelManager.Instance.Perdeu();
                    Debug.Log("Perdeu");
                    feedbackWin.Perdeu();
                    finished = true;
                }
                //StartCoroutine("AcabouFaseOnline");
            }
            ganhouJa = true;

        }


        /*else
        {
            if (coletavelGerador.coletaveis.Length <= 0)
            {
                if (botScore.Value >= 4)
                {
                    feedbackWin.Perdeu();

                    winning.CustomProperties["Ganhador"] = 0;
                    PlayerPrefs.SetInt("GanhouColeta", 0);
                    PlayerPrefs.SetInt("AIGanhou", 1);
                    aiGanhou.Value[2] = true;
                    playerGanhou.Value = false;
                    faseNome = "HUB";
                }

                else
                {
                    feedbackWin.Ganhou();
                    winning.CustomProperties["Ganhador"] = 1;
                    PlayerPrefs.SetInt("GanhouColeta", 1);
                    PlayerPrefs.SetInt("AIGanhou", 0);
                    aiGanhou.Value[2] = false;
                    playerGanhou.Value = true;
                    faseNome = "HistoriaColeta";
                }

                StartCoroutine("AcabouFase");
            }

        }*/
    }

    #endregion

    #region Public Functions

    #endregion

    #region Private Functions

    IEnumerator AcabouFaseOnline()
    {
        moedas.Add(moedasGanhasNessaFase);
        yield return new WaitForSeconds(3f);
        PhotonNetwork.LoadLevel("TelaVitoria");
    }


    IEnumerator AcabouFase()
    {
        FailMessageManager.manualShutdown = true;
        PhotonNetwork.Disconnect();
        moedas.Add(moedasGanhasNessaFase);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(faseNome);
    }

    #endregion

}
