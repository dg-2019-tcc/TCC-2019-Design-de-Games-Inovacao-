using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLocatorOnScreen : MonoBehaviour
{
	public GameObject pointerPrefab;
	private GameObject instance;
	private Camera cam;

	public Vector3 positionAdjust;
	public float adjustToScreen;

	public Transform canvas;

	private Sprite image;

	void Start()
	{

		instance = Instantiate(pointerPrefab, canvas);
		cam = FindObjectOfType<Camera>();
		instance.SetActive(false);

		image = GetComponent<SpriteRenderer>().sprite;
			instance.GetComponentInChildren<PointerName>().sprite = image;
		
	}


	void FixedUpdate()
	{
		instance.transform.position = cam.WorldToScreenPoint(transform.position + positionAdjust);
		if (instance.transform.position.y <= 0 || instance.transform.position.y >= Screen.height || instance.transform.position.x <= 0 || instance.transform.position.x >= Screen.width)
		{
			instance.SetActive(true);
			//Vertical
			if (instance.transform.position.y >= Screen.height)
			{
				instance.transform.position = new Vector3(instance.transform.position.x, Screen.height - adjustToScreen, instance.transform.position.z);
			}
			else if (instance.transform.position.y <= 0)
			{
				instance.transform.position = new Vector3(instance.transform.position.x, adjustToScreen, instance.transform.position.z);
			}
			//Horizontal
			if (instance.transform.position.x >= Screen.width)
			{
				instance.transform.position = new Vector3(Screen.width - adjustToScreen, instance.transform.position.y, instance.transform.position.z);
			}
			else if (instance.transform.position.x <= 0)
			{
				instance.transform.position = new Vector3(adjustToScreen, instance.transform.position.y, instance.transform.position.z);
			}
		}
		else
		{
			instance.SetActive(false);
		}

		instance.transform.up = cam.WorldToScreenPoint(transform.position + positionAdjust) - instance.transform.position;
		//instance.transform.LookAt(transform.position);

	}
}
