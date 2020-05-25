using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxActivate : MonoBehaviour
{


	public GameObject TextBox;
	public GameObject blocker;


	public void PlayerHit()
	{
		if (blocker != null) return;
		TextBox.SetActive(true);
		Destroy(gameObject);

	}
}
