using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour
{

	private PhotonView PV;
	public GameObject myAvatar;
   
    void Start()
    {
		PV = GetComponent<PhotonView>();
		int spawnPicker = Random.Range(0, GameSetupController.GS.spawnPoints.Length);
		if (PV.IsMine)
		{
			if (GameSetupController.GS.testIndex == 1)
			{
				myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar2D"),
					GameSetupController.GS.spawnPoints[spawnPicker].position, GameSetupController.GS.spawnPoints[spawnPicker].rotation, 0);
			}
			if (GameSetupController.GS.testIndex == 2)
			{
				myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"),
					GameSetupController.GS.spawnPoints[spawnPicker].position, GameSetupController.GS.spawnPoints[spawnPicker].rotation, 0);
			}
		}
        
    }

}
