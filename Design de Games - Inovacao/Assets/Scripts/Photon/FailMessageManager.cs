using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class FailMessageManager : MonoBehaviour
{
	[Header ("Objeto pra sinalizar que caiu e quanto tempo mantê-lo na tela")]
	public GameObject message;
	public float timeToWait;

	[Header("Variáveis públicas para visualização e debug")]
	public bool startChecking = false;
	public bool wasConnected;

	//variável estática para chamar facilmente quando o player estiver saindo de propósito, a fim de ter controle da tela que ele vai após desconectar(senão ele vai pra hub)
	public static bool manualShutdown = false;


	private string cena;
	private HistoriaManager taNaCenaDeHist;

    #region Unity Function

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        cena = SceneManager.GetActiveScene().name;

        if (cena == "MenuPrincipal" || cena == "HUB")
        {
            return;
        }


        if (!startChecking)
        {
			startChecking = PhotonNetwork.IsConnected != wasConnected && cena != "MenuPrincipal";
		}
		else
        {
            if (!PhotonNetwork.IsConnected && !manualShutdown && !message.activeSelf && cena != "HUB")
            {
                taNaCenaDeHist = FindObjectOfType<HistoriaManager>();
                if (taNaCenaDeHist == null)
                {
                    manualShutdown = true;
                    startChecking = false;
                    StartCoroutine(WeHaveToGoBack());
                }
                taNaCenaDeHist = null;
            }
            else if (manualShutdown)
            {
                StartCoroutine(resetShutdownMode());
            }

        }
    }

    #endregion

    #region Public Functions

    #endregion

    #region Private Functions

    IEnumerator WeHaveToGoBack()
    {

        message.SetActive(true);

        yield return new WaitForSeconds(timeToWait);
        message.SetActive(false);
        LoadingManager.instance.LoadNewScene(UnityCore.Scene.SceneType.HUB, GameManager.sceneAtual, false);
        //SceneManager.LoadScene("HUB");


    }

    IEnumerator resetShutdownMode()
    {
        yield return new WaitForSeconds(timeToWait);
        manualShutdown = false;

    }

    #endregion

}
