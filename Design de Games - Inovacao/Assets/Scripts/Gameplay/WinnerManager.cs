using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine.SceneManagement;
using UnityCore.Scene;

public class WinnerManager : MonoBehaviour
{
	public PlayerThings player;
	

	[Header("Coletáveis")]

	public bool ganhouCorrida;
	public bool perdeuCorrida;
	

    

    private string faseNome;

	

    SceneType old;

    #region Unity Function
	

    #endregion

    #region Public Functions


    #endregion

    #region Private Functions
	
    

    [PunRPC]
    void TrocaSala()
    {
        Debug.Log("TrocaSala");
        ganhouCorrida = false;
        perdeuCorrida = false;
       
    }

    
	
    
	/*
    IEnumerator Venceu()
    {
        Debug.Log("Venceu");
        player.cameraManager.SendMessage("ActivateCamera", false);
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if ((int)PhotonNetwork.PlayerList[i].CustomProperties["Ganhador"] == 1)
            {
                StopCoroutine(Venceu());
            }
        }
        PhotonNetwork.LocalPlayer.SetScore(-1);
        ganhouCorrida = false;
        yield return new WaitForSeconds(delayForWinScreen);
        PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 1;

        //gameObject.GetComponent<PhotonView>().RPC("ZeraPontuacao", RpcTarget.All);

        pv.RPC("TrocaSala", RpcTarget.All);

    }*/

    #endregion
}
