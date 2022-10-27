using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Complete
{

    public class PhotonPlayer : MonoBehaviour
    {

        private PhotonView PV;

        [HideInInspector]
        public GameObject myAvatar;
        public static GameObject myPlayer;

        public PlayerType playerTypePrefabs;

        public NewPlayerMovent playerMove;
		public NewMotoPlayerMovement motoPlayerMovement;
        public PlayerThings playerThings;

        void Start()
        {
            PV = GetComponent<PhotonView>();

            int spawnPicker;

			spawnPicker = PhotonNetwork.IsMasterClient.Equals(true) ? 0 : 1;  //achar posição de spawn(primeiro ou segundo player)




			//int spawnPicker = Random.Range(0, GameSetupController.GS.spawnPoints.Length);
			string prefabName = GameSetupController.GS.playerPrefabName;

            if (PV.IsMine || !PhotonNetwork.InRoom)
            {

                myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", prefabName),
                    GameSetupController.GS.spawnPoints[spawnPicker].position, GameSetupController.GS.spawnPoints[spawnPicker].rotation, 0);

                myPlayer = myAvatar;
                SceneInitializer.current.GetPlayerPositionInGame();

            }

            playerMove = myAvatar.GetComponentInChildren<NewPlayerMovent>();
			motoPlayerMovement = myAvatar.GetComponentInChildren<NewMotoPlayerMovement>();			
            playerThings = myAvatar.GetComponentInChildren<PlayerThings>();



			if (playerMove != null)
			{
				playerMove.enabled = false;
			}
			if (motoPlayerMovement != null)
			{
				motoPlayerMovement.enabled = false;
			}
        }
    }
}
