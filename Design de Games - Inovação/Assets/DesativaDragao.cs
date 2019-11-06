using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesativaDragao : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dragao"))
        {
            Debug.Log("Desativou");
            other.GetComponent<SpriteRenderer>().enabled = false;
            other.gameObject.layer = 11;
            
        }
    }
}
