using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DelayStartRoomController : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private string waitingRoomSceneIndex;

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
        SceneManager.LoadScene(waitingRoomSceneIndex);

		if (SceneManager.GetActiveScene().name != "DelayStartMenuDemo")
		{
			gameObject.GetComponent<PhotonView>().RPC("TrocaSala", RpcTarget.MasterClient);
		}

	}    
}