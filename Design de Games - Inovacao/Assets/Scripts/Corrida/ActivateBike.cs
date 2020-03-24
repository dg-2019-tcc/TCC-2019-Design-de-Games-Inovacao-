using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBike : MonoBehaviour
{
	public GameObject motoPrefab;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player")) {
			Instantiate(motoPrefab, collision.transform);
		}
	}
}
