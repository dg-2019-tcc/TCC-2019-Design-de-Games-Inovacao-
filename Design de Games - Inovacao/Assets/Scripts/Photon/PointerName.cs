using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PointerName : MonoBehaviour
{

	private TextMeshProUGUI text;
    void Start()
    {
		text = GetComponent<TextMeshProUGUI>();
		text.text = PhotonNetwork.NickName;
    }
	
    void Update()
    {
		transform.up = Vector3.up;
    }
}
