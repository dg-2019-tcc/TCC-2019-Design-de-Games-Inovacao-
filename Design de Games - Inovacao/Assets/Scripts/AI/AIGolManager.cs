using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGolManager : MonoBehaviour
{
    public FloatVariable botScore;

    public GameObject bola;
    public GameObject goool;

    public Transform bolaSpawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Futebol"))
        {
            bola = other.gameObject;

            //GolSelect playerGol = GetComponentInParent<GolSelect>();


            StartCoroutine("ResetaBolaAI");

        }

    }

        IEnumerator ResetaBolaAI()
        {
            goool.SetActive(true);
            bola.SetActive(false);

            //bola.GetComponent<BolaFutebol>().bolaTimer += 5f;

            bola.GetComponent<Rigidbody2D>().isKinematic = true;

            bola.transform.position = bolaSpawnPoint.position;

            botScore.Value++;

            yield return new WaitForSeconds(0.8f);
            goool.SetActive(false);

            bola.SetActive(true);

            bola.GetComponent<Rigidbody2D>().isKinematic = false;

        }

    
}
