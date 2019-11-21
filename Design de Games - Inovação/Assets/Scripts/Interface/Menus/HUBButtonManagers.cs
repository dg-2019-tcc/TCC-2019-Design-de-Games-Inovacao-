using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUBButtonManagers : MonoBehaviour
{
	public GameObject customizationMenu;

	private void OnEnable()
	{
		customizationMenu.SetActive(false);
	}

	private void OnDisable()
	{
		customizationMenu.SetActive(false);
	}

	public void AtivaCustom()
	{
		customizationMenu.SetActive(!customizationMenu.activeSelf);

	}
}
