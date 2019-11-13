using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragaoPlataforma : MonoBehaviour
{
    private Transform target;
    public float speed = 1.0f;


    private void Start()
    {
        target = transform;
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = transform.parent;
        }
    }
}
