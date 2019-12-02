using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// 
    /// Caso a Unity não conecte com uma build o problema geralmente é a configuração de região
    /// pois a build conecta com uma região e o editor da Unity conecta em outra, caso isso aconteça
    /// é necessário somente presetar a região do server (no nosso caso "sa" traduzido para "SouthAmerica")
    /// "FixedRegion" no "PhotonServerSettings", que pode ser encontrado no plugin do Photon, 
    /// lembrando que deve ser utilizado apenas no momento de criação do jogo, e depois deve
    /// ser utilizado normalmente, ou seja, deixar o espaço vazio para que possa ser preenchido normalmente
    /// 
    /// 
    /// ==\Links que podem ser úteis/==
    /// 
    /// Documentação: https://www.youtube.com/redirect?q=https%3A%2F%2Fdoc.photonengine.com%2Fen-us%2Fpun%2Fcurrent%2Fgetting-started%2Fpun-intro&redir_token=qrK4i2UG_lX6RGrtphtnQv-TeBx8MTU2NzYyODc4NkAxNTY3NTQyMzg2&event=video_description&v=02P_mrszvzY
    /// Scripting API: https://www.youtube.com/redirect?q=https%3A%2F%2Fdoc-api.photonengine.com%2Fen%2Fpun%2Fv2%2Findex.html&redir_token=qrK4i2UG_lX6RGrtphtnQv-TeBx8MTU2NzYyODc4NkAxNTY3NTQyMzg2&event=video_description&v=02P_mrszvzY
    /// 
    /// </summary>

    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            return;
        }
        PhotonNetwork.ConnectUsingSettings(); //Conecta com os servidores mestres do photon
    }

   /* public override void OnConnectedToMaster()
    {
        //Basicamente debug para facilitar saber o que acontece aonde
        Debug.Log("Nós estamos conectados ao servidor " + PhotonNetwork.CloudRegion + "!!");
    }*/
}
