using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Complete
{
    public class GameSetupController : MonoBehaviour
    {
		//chamar essa variável para chamar códigos da instância desse código
        public static GameSetupController GS;

		[Header ("Itens principais")]
		public AISpawner aiSpawner;
		public Transform[] spawnPoints;
        public float delayToCreate;
        public float countdown;


        private int indexCountdown = 1;

		[Header ("Countdown na tela antes de começar")] //tava pulando o número 2 no client, não me lembro de termos resolvido
        public GameObject[] number;

        private float allPlayersInSession;

        [HideInInspector] public string playerPrefabName; // vai ser chamado só no código msm e aparece na tela, n tem pq ficar visível
		[HideInInspector] public BoolVariable partidaComecou; // essa dá pra perceber jogando que tá funcionando, é pra referência
        [HideInInspector] static public GameObject PlayerInst; // já tava assim
        [HideInInspector] public PhotonPlayer playerMove; // referência

		[Header ("Marcar qual cena está")]
        public bool isFut;
        public bool isMoto;
        public bool isTutorial;

        #region Unity Function

        private void OnEnable()
        {
            if (GameSetupController.GS == null)
            {
                GameSetupController.GS = this;
            }

            GameManager.pausaJogo = true;
            Debug.Log("Pausa jogo é" + GameManager.pausaJogo);

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

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        #endregion

        #region Public Functions

        #endregion

        #region Private Functions

        [PunRPC]
        private void SpawnPlayer(float alterPlayerCount)
        {
            if (OfflineMode.modoDoOffline && !isTutorial)
            {
                aiSpawner = FindObjectOfType<AISpawner>();
                aiSpawner.SpawnAI();
            }

            if (alterPlayerCount > allPlayersInSession)                                                     //Contador pra sincronizar e adicionar quantos players entraram na cena
                allPlayersInSession = alterPlayerCount;

            allPlayersInSession++;
            if (PhotonNetwork.PlayerList.Length == allPlayersInSession || alterPlayerCount == 0)            //Checando se todos entraram, se sim, todos são criados ao mesmo tempo(se falhar, outro player vai passar pelo mesmo)
            {
                StartCoroutine("UniteSynchronization", delayToCreate);
                //StartCoroutine("SpawnAI");
            }
        }

        private IEnumerator UniteSynchronization(float delay)
        {
            yield return new WaitForSeconds(delay);
            if (number.Length > 0)
            {
                number[number.Length - indexCountdown].SetActive(true);
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Feedback/Contagem/Contagem");
                yield return new WaitForSeconds(1);
                number[number.Length - indexCountdown].SetActive(false);
            }
            if (indexCountdown < number.Length)
                StartCoroutine("UniteSynchronization", 0);
            else
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Feedback/Contagem/Start");
                //PlayerInst.SetActive(true);
                if (playerMove.playerMove != null && isMoto == false)
                {
                    playerMove.playerMove.enabled = true;

                }
                if (playerMove.motoPlayerMovement != null)
                {
                    playerMove.motoPlayerMovement.enabled = true;
                }
                if (OfflineMode.modoDoOffline && !isTutorial)
                {
                    aiSpawner.enabled = true;
                    aiSpawner.comecouPartida = true;
                }

                playerMove.playerThings.comecou = true;
                partidaComecou.Value = true;
                GameManager.pausaJogo = false;
                Debug.Log("Pausa jogo é" + GameManager.pausaJogo);
            }

            indexCountdown++;

        }

        #endregion

	}
}
