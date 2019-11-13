using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragaoPlataforma : MonoBehaviour
{
    public float speed;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector3(speed, 0, 0);
        }
    }
}
