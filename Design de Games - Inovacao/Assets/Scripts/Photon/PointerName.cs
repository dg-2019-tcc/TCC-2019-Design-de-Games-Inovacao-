using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PointerName : MonoBehaviour
{

	private TextMeshProUGUI text;
	[HideInInspector]
	public string nickname;
    void Start()
    {
		text = GetComponent<TextMeshProUGUI>();
		
    }
	
    void Update()
    {
		text.text = nickname;
		transform.up = Vector3.up;
    }
}
