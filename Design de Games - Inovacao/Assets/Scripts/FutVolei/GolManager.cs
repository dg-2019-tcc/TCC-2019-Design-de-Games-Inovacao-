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
	public float moedasGanhasNessaFase;


	private void Start()
    {
        acabou01 = Resources.Load<BoolVariableArray>("Acabou01");
        aiGanhou = Resources.Load<BoolVariableArray>("AIGanhou");
        playerGanhou = Resources.Load<BoolVariable>("PlayerGanhou");
        flowIndex = Resources.Load<FloatVariable>("FlowIndex");
        botScore = Resources.Load<FloatVariable>("BotScore");

        botScore.Value = 0;

        playerGanhou.Value = false;
        aiGanhou.Value[4] = false;

        offline = OfflineMode.modoDoOffline;

        playerGol = GetComponentInParent<GolSelect>();
        bolaPV = bola.GetPhotonView();
    }




    public void PerdeuProBot()
    {
        Debug.Log("Perdeu");
        feedbackWin.Perdeu();
        aiGanhou.Value[4] = true;
        playerGanhou.Value = false;
        PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 0;
        if (isLoading) return;
        isLoading = true;
        StartCoroutine("AcabouFase");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Futebol"))
        {
            /*if (bola == null)
            {
                bola = other.gameObject;
                bolaFutebol = bola.GetComponent<BolaFutebol>();
            }*/
            playerGol.jogador.PV.Owner.AddScore(1);



            bolaFutebol.FoiGol();



            if (playerGol.jogador.PV.Owner.GetScore() >= 5)
            {
                feedbackWin.Ganhou();
                aiGanhou.Value[4] = false;
                playerGanhou.Value = true;
				PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 1;
				if (isLoading) return;
				isLoading = true;
                //StartCoroutine("AcabouFase");
                Acaba();
            }
            
        }

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
				Acaba();
			}
		}
	}

	[PunRPC]
    void Acaba()
    {
        StartCoroutine("AcabouFase");
    }

    IEnumerator AcabouFase()
    {
        isLoading = true;
        yield return new WaitForSeconds(3f);

		if (!PhotonNetwork.OfflineMode)
		{
			PhotonNetwork.LoadLevel("TelaVitoria");


		}
		else
		{
			if (aiGanhou.Value[4] == true)
			{
				if (acabou01.Value[4] == true)
				{
					LevelManager.Instance.GoPodium();
					//PhotonNetwork.LoadLevel("TelaVitoria");

				}
				else
				{
                    FailMessageManager.manualShutdown = true;
                    PhotonNetwork.Disconnect();
                    LevelManager.Instance.GoHub();
				}
			}

			else if (playerGanhou.Value == true)
			{
				if (acabou01.Value[4] == true)
				{
					PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 1;
					LevelManager.Instance.GoPodium();
					//PhotonNetwork.LoadLevel("TelaVitoria");
				}
				else
				{
					LevelManager.Instance.HistFutebol();
					//flowIndex.Value = 4;
				}

				moedas.Add(moedasGanhasNessaFase);
			}
		}
    }

    [PunRPC]
    public void Recomeca()
    {
        StartCoroutine("ResetaBola");
    }

    [PunRPC]
    public void DesativaBola()
    {
        Debug.Log("Desativa");
        desativaBola = true;
        ativaBola = false;
    }

    [PunRPC]
    public void AtivaBola()
    {
        Debug.Log("Ativa");
        desativaBola = false;
        ativaBola = true;
    }


    /*IEnumerator ResetaBola()
    {

           /* Debug.Log("Reseta");
            goool.SetActive(true);
            bola.SetActive(false);

            //bola.GetComponent<BolaFutebol>().bolaTimer += 5f;

            bola.GetComponent<Rigidbody2D>().isKinematic = true;

            bola.transform.position = bolaSpawnPoint.position;
    

        if (ativaBola)
        {

            bola.SetActive(true);

            bola.GetComponent<Rigidbody2D>().isKinematic = false;
            goool.SetActive(false);
        }
    


    bolaPV.RPC("DesativaBola", RpcTarget.All);

        index++;

        yield return new WaitForSeconds(0.8f);
        //placarText.text = playerGol.jogador.PV.Owner.GetScore().ToString();

        bolaPV.RPC("AtivaBola", RpcTarget.All);*/

    //}
}
