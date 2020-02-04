using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DelayStartRoomController : MonoBehaviourPunCallbacks
{

    [Header("Nome da sala de espera")]

    [SerializeField]
    private string SalaDeEsperaNomeDaCena;



    [Header("Nome do menu de customização")]

    [SerializeField]
    private string menuCustomizacao;



    [Header("Scripts Externos")]

    public DelayStartLobbyController roomController;



    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene(SalaDeEsperaNomeDaCena);

		if (SceneManager.GetActiveScene().name != menuCustomizacao)
		{
			gameObject.GetComponent<PhotonView>().RPC("TrocaSala", RpcTarget.MasterClient);
		}
	}    
}