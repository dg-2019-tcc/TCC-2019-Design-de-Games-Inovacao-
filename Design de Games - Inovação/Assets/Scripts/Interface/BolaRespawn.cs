using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaRespawn : MonoBehaviour
{
	public Transform referencePosition;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		collision.transform.position = referencePosition.position;
	}
}
