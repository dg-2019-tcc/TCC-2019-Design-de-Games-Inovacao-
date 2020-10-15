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
    public GameObject[] grafites;
    public GameObject player;
    public GameObject playerMovement;
	public Transform playerPos;
    public GameObject cam;

    public float delay;
    private int index;
    public bool isDone;
	private float minSpawnPosition = 15f;
    private bool shouldDeactivateRuntime;

    #region Unity Function
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
    #endregion

    #region Public Functions
    public void GetPlayerPositionInGame()
    {
        playerMovement = PhotonPlayer.myPlayer;
        playerPos = playerMovement.transform.GetChild(1);
    }
    #endregion

    #region Private Functions
    private void Spawn()
    {
        if (!objsIni[objsIni.Length - 1].activeSelf)
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
        yield return new WaitForSeconds(1f);
        GameManager.pausaJogo = false;
        cam.SetActive(true);
        if (player != null)
        {
            player.SetActive(true);
            if (GameManager.sceneAtual == SceneType.HUB)
            {
                GetPlayerPositionHUB();
            }
        }
        isDone = true;
        if (PageController.instance.entryPage != PageType.None)
        {
            PageController.instance.TurnPageOn(PageController.instance.entryPage);
        }
        //GarbageController.DisableGC();
    }

    private void FixedUpdate()
    {
        if (isDone == false) return;
        if (shouldDeactivateRuntime)
        {
            DeactivateAtRuntime();
        }
    }

    private void DeactivateAtRuntime()
    {
        foreach (GameObject objs in grafites)
        {
            if (Vector3.Distance(playerPos.position, objs.transform.position) < minSpawnPosition)
            {
                objs.SetActive(true);
            }
            else if (objs.activeSelf)
            {
                //GarbageController.callIndex++;
                objs.SetActive(false);
            }
        }
    }

    private void CheckFase()
    {
        switch (GameManager.sceneAtual)
        {
            case SceneType.Coleta:
                shouldDeactivateRuntime = true;
                break;

            case SceneType.HUB:
                shouldDeactivateRuntime = true;
                break;

            default:
                shouldDeactivateRuntime = false;
                break;
        }
        InvokeRepeating("Spawn", delay, delay);
    }

    private void GetPlayerPositionHUB()
    {
        playerPos = player.transform.GetChild(1);
    }
    #endregion
}
