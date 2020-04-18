using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGolManager : MonoBehaviour
{
    public FloatVariable botScore;

    public GameObject bola;

    public Transform bolaSpawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bola"))
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

            yield return new WaitForSeconds(3f);

            bola.SetActive(true);

            bola.GetComponent<Rigidbody2D>().isKinematic = false;

        }

    
}
