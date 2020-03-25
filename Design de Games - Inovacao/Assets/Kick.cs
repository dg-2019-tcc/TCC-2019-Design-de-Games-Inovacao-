using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kick : MonoBehaviour
{

    public float cooldownKick;

    public float kickSize;

    public float kickForcex;

    private bool kicked;

    private Rigidbody2D ballrb;


    public void KickBall()
    {
        if (kicked == false)
        {
            StartCoroutine("CoolKick");
        }
    }

    IEnumerator CoolKick()
    {
        transform.position = transform.position + new Vector3(kickSize, 0, 0);
        kicked = true;

        yield return new WaitForSeconds(cooldownKick);

        transform.position = transform.position + new Vector3(-kickSize, 0, 0);
        kicked = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Bola") && kicked == true)
        {
            ballrb = col.GetComponent<Rigidbody2D>();

            ballrb.AddForce(new Vector2(kickForcex, Random.Range(-5f,5f)), ForceMode2D.Impulse);
        }
    }
}
