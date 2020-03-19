using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLocatorOnScreen : MonoBehaviour
{
	public GameObject pointerPrefab;
	private GameObject instance;
	private Camera cam;

	public Vector3 positionAdjust;
    
    void Start()
    {
		
		instance = Instantiate(pointerPrefab, GetComponent<PlayerMovement>().canvasSelf.transform);
		cam = FindObjectOfType<Camera>();
		instance.SetActive(false);
    }

    
    void FixedUpdate()
    {
		instance.transform.position = cam.WorldToScreenPoint(transform.position + positionAdjust);
		
    }
}
