using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailDirection : MonoBehaviour
{
	private Vector3 oldPos;

    
    void Update()
    {
		transform.right = transform.position - oldPos;
		oldPos = transform.position;
    }
}
