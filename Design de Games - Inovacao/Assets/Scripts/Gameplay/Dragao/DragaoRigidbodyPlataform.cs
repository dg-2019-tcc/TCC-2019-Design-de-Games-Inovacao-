using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragaoRigidbodyPlataform : MonoBehaviour
{
    public List<Rigidbody2D> rigidbodies = new List<Rigidbody2D>();

    public Vector3 lastPosition;

    Transform _transform;
    
    
    private void Start()
    {
        _transform = transform;
        lastPosition = _transform.position;
    }


    private void LateUpdate()
    {
        if(rigidbodies.Count > 0)
        {
            for (int i = 0; i < rigidbodies.Count; i++)
            {
                Rigidbody2D rb = rigidbodies[i];
                Vector3 velocity = (_transform.position - lastPosition);
                rb.transform.Translate(velocity, _transform);
            }
        }
        lastPosition = _transform.position;
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.GetComponentInParent<Rigidbody2D>();
        if (rb != null && collision.CompareTag("GroundCheck"))
        {
            Add(rb);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("rola");
        Rigidbody2D rb = collision.GetComponentInParent<Rigidbody2D>();
        if (rb != null && collision.CompareTag("GroundCheck"))
        {
            Remove(rb);
        }
    }

    public void Add(Rigidbody2D rb)
    {
        if (!rigidbodies.Contains(rb))
        {
            rigidbodies.Add(rb);
        }
    }

    public void Remove(Rigidbody2D rb)
    {
        if (rigidbodies.Contains(rb))
        {
            rigidbodies.Remove(rb);
        }
    }






}
