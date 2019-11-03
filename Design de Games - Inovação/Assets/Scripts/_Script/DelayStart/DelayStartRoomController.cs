using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DelayStartRoomController : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private string waitingRoomSceneIndex;

    [SerializeField]
    private string waitingRoomTutorialSceneIndex;

    public DelayStartLobbyController lobbyController;

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
        if(lobbyController.gameRoom == true)
        {
            SceneManager.LoadScene(waitingRoomSceneIndex);
        }
        else if(lobbyController.tutorialRoom == true)
        {
            SceneManager.LoadScene(waitingRoomTutorialSceneIndex);
        }

		if (SceneManager.GetActiveScene().name != "DelayStartMenuDemo")
		{
			gameObject.GetComponent<PhotonView>().RPC("TrocaSala", RpcTarget.MasterClient);
		}
	}    
}