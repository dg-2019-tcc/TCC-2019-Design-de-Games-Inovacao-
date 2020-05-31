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

    public BoolVariable acabou01;
    public FloatVariable flowIndex;
    public FloatVariable botScore;

    private string faseNome;

    private bool acabouTutorial;
    private int indexTutorial;

	private bool isEmpatado;

    public BoolVariable aiGanhou;
    public BoolVariable playerGanhou;

    public FeedbackText feedbackWin;

	

	private void Start()
	{
        indexTutorial = PlayerPrefs.GetInt("Fase");
        PlayerPrefs.SetInt("GanhouColeta", 0);
        if(indexTutorial == 8)
        {
            acabouTutorial = true;
        }

		coletavelGerador = FindObjectOfType<ColetavelGerador>();
        aiGanhou = Resources.Load<BoolVariable>("AIGanhou");
        playerGanhou = Resources.Load<BoolVariable>("PlayerGanhou");

        aiGanhou.Value = false;
        playerGanhou.Value = false;

        winning = PhotonNetwork.PlayerList[0];
		ganhouJa = false;
		isEmpatado = true;
	}


	void Update()
    {
        if (acabouTutorial == true)
        {
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

                    winning.CustomProperties["Ganhador"] = 0;
                    feedbackWin.Perdeu();
                    StartCoroutine("AcabouFaseOnline");

                }
                else
                {
                    feedbackWin.Ganhou();
                    winning.CustomProperties["Ganhador"] = 1;
                    StartCoroutine("AcabouFaseOnline");
                }
                ganhouJa = true;

            }   
        }

        else
        {
            if (coletavelGerador.coletaveis.Length <= 0)
            {
                if (botScore.Value >= 4)
                {
                    feedbackWin.Perdeu();
                    winning.CustomProperties["Ganhador"] = 0;
                    PlayerPrefs.SetInt("GanhouColeta", 0);
                    aiGanhou.Value = true;
                    playerGanhou.Value = false;
                    faseNome = "HUB";
                }

                else
                {
                    feedbackWin.Ganhou();
                    winning.CustomProperties["Ganhador"] = 1;
                    PlayerPrefs.SetInt("GanhouColeta", 1);
                    aiGanhou.Value = false;
                    playerGanhou.Value = true;
                    faseNome = "HistoriaColeta";
                }

                StartCoroutine("AcabouFase");
            }

        }
	}

    IEnumerator AcabouFaseOnline()
    {
        yield return new WaitForSeconds(3f);
        PhotonNetwork.LoadLevel("TelaVitoria");
    }


    IEnumerator AcabouFase()
    {

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(faseNome);
    }

	


	
}
