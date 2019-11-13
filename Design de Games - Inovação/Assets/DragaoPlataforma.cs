using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragaoPlataforma : MonoBehaviour
{
    private Transform target;
    public float speed = 10.0f;


    private void Start()
    {
        target = transform;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            float step = speed * Time.deltaTime;
            other.transform.position = Vector3.MoveTowards(other.transform.position, target.position, step);
        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            float step = speed * Time.deltaTime;
            other.transform.position = Vector3.MoveTowards(other.transform.position, target.position, step);
        }
    }
}
