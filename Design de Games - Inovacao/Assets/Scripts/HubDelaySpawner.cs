using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubDelaySpawner : MonoBehaviour
{
    public GameObject[] coisasHub;
    public GameObject player;

    public GameObject fadeIn;

    private int index;

    public float delay;

    private bool finished;

    void Start()
    {
        index = 0;
        //StartCoroutine(Spawn());
        InvokeRepeating("Spawn", delay, delay);
    }


    //private IEnumerator Spawn()
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
        yield return new WaitForSeconds(0.5f);
        fadeIn.SetActive(false);
    }
}
