using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditosManager : MonoBehaviour
{
	public string nomeDoMenu;
	public void VoltaMenu()
	{
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
        SceneManager.LoadScene(nomeDoMenu);
	}
}
