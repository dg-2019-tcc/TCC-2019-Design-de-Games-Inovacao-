using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragaoSwitch : MonoBehaviour
{
    [Header ("Switch")]

    [SerializeField]
    private bool switchDragao;
    [SerializeField]
    private int layerDragao;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dragao"))
        {
            other.GetComponent<SpriteRenderer>().enabled = switchDragao;
            other.gameObject.layer = layerDragao;
        }
    }
}
