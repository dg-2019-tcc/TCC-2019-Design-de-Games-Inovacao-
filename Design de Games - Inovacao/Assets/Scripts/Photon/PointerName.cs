using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class PointerName : MonoBehaviour
{

	private TextMeshProUGUI text;
	private Image image;

	public bool straight = false;


	[HideInInspector]
	public string nickname;
	[HideInInspector]
	public Sprite sprite;
    void Start()
    {
		text = GetComponent<TextMeshProUGUI>();
		image = GetComponent<Image>();
		
    }
	
    void Update()
    {
		if (text != null)
		{
			text.text = nickname;
			transform.up = Vector3.up;
		}
		else if (image != null)
		{
			image.sprite = sprite;
			if (straight)
			{
				transform.up = Vector3.up;
			}
			else
			{
				transform.up = Vector3.up - Vector3.right;
			}
		}
		
    }
}
