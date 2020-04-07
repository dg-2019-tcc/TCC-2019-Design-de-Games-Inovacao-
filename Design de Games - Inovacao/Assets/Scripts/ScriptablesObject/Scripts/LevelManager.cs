using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : GameManager
{
    public new static LevelManager Instance => GameManager.Instance as LevelManager;

    //public FloatVariable CurrentLevelIndex;

    public int coletaMax = 7;
    



    public void GoHub()
    {
        //CurrentLevelIndex.Value = 0;
    }

    public void GoColeta()
    {
        //CurrentLevelIndex.Value = 1;
    }

    public void GoCorrida()
    {
        //CurrentLevelIndex.Value = 2;
    }

    [PunRPC]
     public void GoVitoria()
    {
        PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 1;
        //CurrentLevelIndex.Value = 3;
        gameObject.GetComponent<PhotonView>().RPC("GoPodium", RpcTarget.All);
        // Manda o jogador q ganhou pra tela como vitorioso
        // e ativa o GoDerrota() para todos os outros
    }

    [PunRPC]
    void GoDerrota()
    {
        PhotonNetwork.LocalPlayer.CustomProperties["Ganhador"] = 0;
        //Só é ativado quando alguem ganha
        //CurrentLevelIndex.Value = 3;
        gameObject.GetComponent<PhotonView>().RPC("GoPodium", RpcTarget.MasterClient);
    }

    [PunRPC]
    public void GoPodium()
    {
        PhotonNetwork.LoadLevel("TelaVitoria");
    }
}
