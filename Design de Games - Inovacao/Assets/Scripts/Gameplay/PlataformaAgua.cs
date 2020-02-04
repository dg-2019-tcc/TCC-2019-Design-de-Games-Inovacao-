using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaAgua : MonoBehaviour
{
    public Transform target;
    public Transform startPos;
    public float speed = 1.0f;
	
    void Update()
    {
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        if (Vector3.Distance(transform.position, target.position) < 0.001f)
        {
            transform.position = startPos.transform.position;
        }
    }
}
