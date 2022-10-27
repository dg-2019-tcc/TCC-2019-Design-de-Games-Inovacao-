using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityCore.Scene;

public class LinhaDeChegada : MonoBehaviour
{
    PhotonView playerView;

    public static bool finished;

    public int totalPlayers;

    public static bool changeRoom = false;

    public bool euAcabei = false;
	

	[Header ("Variáveis das Moedas ganhas e feedback")]
	public int moedasGanhasNessaFase;
	public FeedbackText feedback;


	#region Unity Function

	public void Start()
    {
        euAcabei = false;
        finished = false;
		feedback = FindObjectOfType<FeedbackText>();		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AI"))
        {
			PerdeuCorrida();
            LevelManager.Instance.Perdeu(moedasGanhasNessaFase);
            other.GetComponent<StateController>().enabled = false;
        }
    }

    #endregion

    #region Public Functions

    public void AIGanhou()
    {
		PerdeuCorrida();
        LevelManager.Instance.Perdeu(moedasGanhasNessaFase);
    }

    public void Colidiu(GameObject other)
    {
        playerView = other.GetComponent<PhotonView>();
        if (finished == false)
        {
            if (playerView.IsMine == true && euAcabei == false)
            {
                LevelManager.Instance.Ganhou(moedasGanhasNessaFase);
				GanhouCorrida();
                totalPlayers++;
                euAcabei = true;
                changeRoom = true;
            }
        }
        else
        {
            if (playerView.IsMine == true && euAcabei == false)
            {
                totalPlayers++;
                euAcabei = true;
            }
        }
    }

    [PunRPC]
    public void Acabou()
    {
        finished = true;
    }

	#endregion

	#region Private Functions

	private void GanhouCorrida()
	{
		feedback.Ganhou();
		LevelManager.Instance.Ganhou(moedasGanhasNessaFase);
	}

	private void PerdeuCorrida()
	{

		feedback.Perdeu();
		LevelManager.Instance.Perdeu(moedasGanhasNessaFase);
	}

	#endregion
}
