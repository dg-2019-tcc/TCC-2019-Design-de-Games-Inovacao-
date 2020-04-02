using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUBButtonManagers : MonoBehaviour
{
	public GameObject customizationMenu;
	public GameObject tutorialButton;

	private void OnEnable()
	{
		customizationMenu.SetActive(false);
		
	}

	private void OnDisable()
	{
		customizationMenu.SetActive(false);
		tutorialButton.SetActive(true);
	}

	public void AtivaCustom()
	{
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Roupas", GetComponent<Transform>().position);
        customizationMenu.SetActive(!customizationMenu.activeSelf);
		tutorialButton.SetActive(!customizationMenu.activeSelf);

	}
}
