using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaVoleiStart : MonoBehaviour
{

    public GameObject bola;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StartVolei");   
    }

    IEnumerator StartVolei()
    {
        bola.GetComponent<Rigidbody2D>().isKinematic = true;

        yield return new WaitForSeconds(6f);


        bola.GetComponent<Rigidbody2D>().isKinematic = false;
    }
}
