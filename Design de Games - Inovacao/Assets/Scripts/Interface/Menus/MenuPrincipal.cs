﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField]
    private string nomeDoMenu;
	[SerializeField]
	private string nomeDosCreditos;

    public void ComecaJogo()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Start", GetComponent<Transform>().position);
        SceneManager.LoadScene(nomeDoMenu);
    }

	public void Creditos()
	{
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
        SceneManager.LoadScene(nomeDosCreditos);
	}
}
