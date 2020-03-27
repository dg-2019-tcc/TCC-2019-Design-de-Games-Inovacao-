using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour
{

	private PhotonView PV;

    [HideInInspector]
	public GameObject myAvatar;

    public PlayerType playerTypePrefabs;
   
    void Start()
    {
		PV = GetComponent<PhotonView>();
		int spawnPicker = Random.Range(0, GameSetupController.GS.spawnPoints.Length);
        string prefabName = GameSetupController.GS.playerPrefabName;

        if (PV.IsMine || !PhotonNetwork.InRoom)
		{

            myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", prefabName),
                GameSetupController.GS.spawnPoints[spawnPicker].position, GameSetupController.GS.spawnPoints[spawnPicker].rotation, 0);
			
		}
        
    }

}
