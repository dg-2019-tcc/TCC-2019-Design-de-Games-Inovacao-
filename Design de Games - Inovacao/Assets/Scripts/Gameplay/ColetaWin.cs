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

	

	private void Start()
	{
        indexTutorial = PlayerPrefs.GetInt("Fase");
        PlayerPrefs.SetInt("GanhouColeta", 0);
        if(indexTutorial == 8)
        {
            acabouTutorial = true;
        }

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
					Debug.Log("perdeu pro bot");
					feedbackWin.Perdeu();
					winning.CustomProperties["Ganhador"] = 0;
					PlayerPrefs.SetInt("GanhouColeta", 0);
					PlayerPrefs.SetInt("AIGanhou", 1);
					aiGanhou.Value[1] = true;
					playerGanhou.Value = false;
					faseNome = "HUB";
					StartCoroutine("AcabouFase");

				}
                else
                {
					if (PhotonNetwork.LocalPlayer == winning)
					{
						Debug.Log("Ganhou");
						feedbackWin.Ganhou();
						winning.CustomProperties["Ganhador"] = 1;
					}
					else
					{
						Debug.Log("Perdeu");
						feedbackWin.Perdeu();
					}
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
                    PlayerPrefs.SetInt("AIGanhou", 1);
                    aiGanhou.Value[1] = true;
                    playerGanhou.Value = false;
                    faseNome = "HUB";
                }

                else
                {
                    feedbackWin.Ganhou();
                    winning.CustomProperties["Ganhador"] = 1;
                    PlayerPrefs.SetInt("GanhouColeta", 1);
                    PlayerPrefs.SetInt("AIGanhou", 0);
                    aiGanhou.Value[1] = false;
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
