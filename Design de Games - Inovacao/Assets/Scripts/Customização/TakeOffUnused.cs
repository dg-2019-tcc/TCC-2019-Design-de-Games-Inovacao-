using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOffUnused : MonoBehaviour
{
	public bool letThemBeOn = false;

	private void Start()
	{
		CheckAndExecute();
	}

	private void Update()
	{
		CheckAndExecute();
		letThemBeOn = true;
	}

	private void CheckAndExecute()
	{
		if (letThemBeOn)
		{
			return;

		}

		foreach (Transform part in GetComponentsInChildren<Transform>(true))
		{
			ChangeMultipleCustom changeMultipleCustom = part.GetComponent<ChangeMultipleCustom>();
			if (!part.gameObject.activeSelf && changeMultipleCustom != null)
			{
				foreach  (GameObject subPart in changeMultipleCustom.multipleCustom)
				{
					Destroy(subPart);
				}
			}
		}
	}
}
