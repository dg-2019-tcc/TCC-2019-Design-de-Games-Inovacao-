using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubDelaySpawner : MonoBehaviour
{
    public static HubDelaySpawner current;

    public GameObject[] coisasHub;
    public GameObject player;
    public GameObject cam;

    public GameObject fadeIn;

    private int index;

    public float delay;

    public bool isDone;

    void Awake()
    {
        current = this;
    }

    private void Start()
    {
        GameManager.pausaJogo = true;
        index = 0;
        InvokeRepeating("Spawn", delay, delay);
    }


    private void Spawn()
    {
        if (!coisasHub[coisasHub.Length - 1].activeSelf)
        {

            coisasHub[index].SetActive(true);
            index++;

            //yield return new WaitForSeconds(delay);
            //StartCoroutine(Spawn());
        }
        else
        {
            StartCoroutine("StartHub");
            CancelInvoke();
        }
    }

    private IEnumerator StartHub()
    {
        yield return new WaitForSeconds(1f);
        isDone = true;
        //fadeIn.SetActive(false);
        GameManager.pausaJogo = false;
        cam.SetActive(true);
        Debug.Log("StartHUB");
        player.SetActive(true);
    }
}
