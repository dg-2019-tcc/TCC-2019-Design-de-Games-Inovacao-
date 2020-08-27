using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.Menu;

public class SceneInitializer : MonoBehaviour
{
    public static SceneInitializer current;

    public GameObject[] objsIni;
    public GameObject player;
    public GameObject cam;

    public float delay;
    private int index;
    public bool isDone;

    void Awake()
    {
        current = this;
    }

    private void Start()
    {
        isDone = false;
        GameManager.pausaJogo = true;
        InvokeRepeating("Spawn", delay, delay);
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
        if (player != null) { player.SetActive(true); }
        isDone = true;
        if (PageController.instance.entryPage != PageType.None)
        {
            PageController.instance.TurnPageOn(PageController.instance.entryPage);
        }
        Debug.Log("[SceneInitializer] Initialize Scene Done");
    }
}
