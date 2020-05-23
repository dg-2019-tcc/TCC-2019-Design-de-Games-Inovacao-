using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Complete
{
    public class GameSetupController : MonoBehaviour
    {
        public GameManager gameManager;

        public static GameSetupController GS;

        public Transform[] spawnPoints;

        public float delayToCreate;
        public float countdown;

        private int index = 1;

        public GameObject[] number;

        private float allPlayersInSession;

        public string playerPrefabName;

        public BoolVariable partidaComecou;

        [HideInInspector]
        static public GameObject PlayerInst;
        public PhotonPlayer playerMove;

        public bool isFut;
        public bool isMoto;

        public float aiSpawnCooldown;


        private void OnEnable()
        {
            if (GameSetupController.GS == null)
            {
                GameSetupController.GS = this;
            }

            if (isFut)
            {
                if (PhotonNetwork.IsMasterClient.Equals(true))
                {
                    PlayerInst = (GameObject)PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"),
                                   spawnPoints[0].position, Quaternion.identity);
                    //PlayerInst.SetActive(false);
                    playerMove = PlayerInst.GetComponent<PhotonPlayer>();

                    gameObject.GetComponent<PhotonView>().RPC("SpawnPlayer", RpcTarget.All, allPlayersInSession);
                }

                else
                {
                    PlayerInst = (GameObject)PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"),
                                   spawnPoints[1].position, Quaternion.identity);
                    //PlayerInst.SetActive(false);
                    playerMove = PlayerInst.GetComponent<PhotonPlayer>();


                    gameObject.GetComponent<PhotonView>().RPC("SpawnPlayer", RpcTarget.All, allPlayersInSession);
                    Debug.Log("isClient");
                }
            }
            else
            {
                PlayerInst = (GameObject)PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"),
                                   spawnPoints[0].position, Quaternion.identity);
                //PlayerInst.SetActive(false);
                playerMove = PlayerInst.GetComponent<PhotonPlayer>();

                if (!PhotonNetwork.InRoom)
                {
                    SpawnPlayer(0);
                }
                else
                {
                    gameObject.GetComponent<PhotonView>().RPC("SpawnPlayer", RpcTarget.All, allPlayersInSession);
                }

                //Debug.Log("NãoÉFut");
            }
        }


        [PunRPC]
        private void SpawnPlayer(float alterPlayerCount)
        {
            if (alterPlayerCount > allPlayersInSession)                                                     //Contador pra sincronizar e adicionar quantos players entraram na cena
                allPlayersInSession = alterPlayerCount;

            allPlayersInSession++;
            if (PhotonNetwork.PlayerList.Length == allPlayersInSession || alterPlayerCount == 0)            //Checando se todos entraram, se sim, todos são criados ao mesmo tempo(se falhar, outro player vai passar pelo mesmo)
            {
                StartCoroutine("UniteSynchronization", delayToCreate);
                StartCoroutine("SpawnAI");
            }
        }

        public IEnumerator UniteSynchronization(float delay)
        {
            yield return new WaitForSeconds(delay);
            if (number.Length > 0)
            {
                number[number.Length - index].SetActive(true);
                yield return new WaitForSeconds(1);
                number[number.Length - index].SetActive(false);
            }
			if (index < number.Length)
				StartCoroutine("UniteSynchronization", 0);
			else
			{
				//PlayerInst.SetActive(true);
				if (playerMove.playerMove != null && isMoto == false)
				{
					playerMove.playerMove.enabled = true;
				}
				if (playerMove.motoPlayerMovement != null)
				{
					playerMove.motoPlayerMovement.enabled = true;
				}
				playerMove.playerThings.comecou = true;
				partidaComecou.Value = true;
			}
			
            index++;

        }

        public IEnumerator SpawnAI()
        {
            yield return new WaitForSeconds(aiSpawnCooldown);
			if (OfflineMode.modoDoOffline)
			{
				gameManager.SpawnAI();
			}
        }

		private void OnDestroy()
		{
			StopAllCoroutines();
		}
	}
}
