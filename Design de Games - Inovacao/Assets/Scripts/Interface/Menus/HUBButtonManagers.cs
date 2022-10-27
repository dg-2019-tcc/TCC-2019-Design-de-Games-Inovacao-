using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUBButtonManagers : MonoBehaviour
{
	public GameObject customizationMenu;
	public GameObject tutorialButton;

    public Joystick joy;

    public string nomeDoMenu;



    /*private void OnEnable()
	{
		customizationMenu.SetActive(false);
		
	}

	private void OnDisable()
	{
		customizationMenu.SetActive(false);
		tutorialButton.SetActive(true);
	}*/

    public void AtivaCustom()
	{
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Roupas", GetComponent<Transform>().position);
        SceneManager.LoadScene(nomeDoMenu);
        /*customizationMenu.SetActive(!customizationMenu.activeSelf);
		tutorialButton.SetActive(!customizationMenu.activeSelf);*/

    }
}
