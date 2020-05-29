﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIVoleiManager : MonoBehaviour
{
    public FloatVariable botScore;

    public GameObject bola;
    public Transform bolaSpawnPoint;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Volei"))
        {
            bola = other.gameObject;

            //GolSelect playerGol = GetComponentInParent<GolSelect>();


            StartCoroutine("ResetaBolaAI");

        }

    }

    IEnumerator ResetaBolaAI()
    {
        bola.SetActive(false);

        //bola.GetComponent<BolaFutebol>().bolaTimer += 5f;

        bola.GetComponent<Rigidbody2D>().isKinematic = true;

        bola.transform.position = bolaSpawnPoint.position;

        botScore.Value++;

        yield return new WaitForSeconds(0.8f);

        bola.SetActive(true);

        bola.GetComponent<Rigidbody2D>().isKinematic = false;

    }
}