using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivaDragao : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dragao"))
        {
            other.GetComponent<SpriteRenderer>().enabled = true;
            other.gameObject.layer = 0;
        }
    }
}
