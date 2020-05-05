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

        public PlayerType playerTypePrefabs;

        public NewPlayerMovent playerMove;
		public NewMotoPlayerMovement motoPlayerMovement;
        public PlayerThings playerThings;
        public NewMotoPlayerMovement motoMove;

        void Start()
        {
            PV = GetComponent<PhotonView>();

            int spawnPicker;


            if (PhotonNetwork.IsMasterClient.Equals(true))
            {
                spawnPicker = 0;
            }

            else
            {
                spawnPicker = 1;
            }



            //int spawnPicker = Random.Range(0, GameSetupController.GS.spawnPoints.Length);
            string prefabName = GameSetupController.GS.playerPrefabName;

            if (PV.IsMine || !PhotonNetwork.InRoom)
            {

                myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", prefabName),
                    GameSetupController.GS.spawnPoints[spawnPicker].position, GameSetupController.GS.spawnPoints[spawnPicker].rotation, 0);

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
