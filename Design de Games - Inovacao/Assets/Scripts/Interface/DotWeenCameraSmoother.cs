using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotWeenCameraSmoother : MonoBehaviour
{
	private Camera cam;
	private Canvas canvas;

    private void Awake()
    {
		canvas = GetComponent<Canvas>();
    }
	
    private void Update()
    {
		canvas.worldCamera = Camera.main;
	}

}
