using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.Menu;
using UnityCore.Scene;
using Complete;

public class SceneInitializer : MonoBehaviour
{
    public static SceneInitializer current;

    public GameObject[] objsIni;
    public GameObject player;
    public GameObject playerMovement;
	public Transform playerPos;
    public GameObject cam;

    public float delay;
    private int index;
    public bool isDone;
	private float minSpawnPosition = 10f;
    private bool shouldDeactivateRuntime;
    private bool isGame;

    void Awake()
    {
        current = this;
    }

    private void Start()
    {
        isDone = false;
        GameManager.pausaJogo = true;

        CheckFase();
    }

    private void Spawn()
    {
		
        if(!objsIni[objsIni.Length - 1].activeSelf)
        {
            objsIni[index].SetActive(true);
            index++;
        }
        else
        {
            StartCoroutine("StartScene");
            CancelInvoke();
        }
    }

    private IEnumerator StartScene()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.pausaJogo = false;
        cam.SetActive(true);
        if (player != null)
        {
            player.SetActive(true);
            if(GameManager.sceneAtual == SceneType.HUB)
            {
                GetPlayerPositionHUB();
            }
        }
        isDone = true;
        if (PageController.instance.entryPage != PageType.None)
        {
            PageController.instance.TurnPageOn(PageController.instance.entryPage);
        }
        Debug.Log("[SceneInitializer] Initialize Scene Done");
    }

	private void Update()
	{
        if(isDone == false)return;
        if (shouldDeactivateRuntime)
        {
            DeactivateAtRuntime();
        }
	}

    private void DeactivateAtRuntime()
    {
        foreach (GameObject grafite in objsIni)
        {
            if (Vector3.Distance(playerPos.position, grafite.transform.position) < minSpawnPosition)
            {
                grafite.SetActive(true);
            }
            else if (grafite.activeSelf)
            {
                grafite.SetActive(false);
            }
        }
    }

    private void CheckFase()
    {
        switch (GameManager.sceneAtual)
        {
            case SceneType.Corrida:
                shouldDeactivateRuntime = true;
                isGame = true;
                break;

            case SceneType.Moto:
                shouldDeactivateRuntime = true;
                isGame = true;
                break;

            case SceneType.Coleta:
                shouldDeactivateRuntime = true;
                isGame = true;
                break;

            case SceneType.Futebol:
                shouldDeactivateRuntime = false;
                isGame = true;
                break;

            case SceneType.Volei:
                shouldDeactivateRuntime = false;
                isGame = true;
                break;

            case SceneType.HUB:
                shouldDeactivateRuntime = true;
                isGame = false;
                break;

            default:
                shouldDeactivateRuntime = false;
                isGame = false;
                break;
        }

        if (shouldDeactivateRuntime)
        {
            StartCoroutine("StartScene");
        }
        else
        {
            InvokeRepeating("Spawn", delay, delay);
        }
    }

    public void GetPlayerPositionInGame()
    {
        playerMovement = PhotonPlayer.myPlayer;
        playerPos = playerMovement.transform.GetChild(1);
    }

    private void GetPlayerPositionHUB()
    {
        playerPos = player.transform.GetChild(1);
    }
}
