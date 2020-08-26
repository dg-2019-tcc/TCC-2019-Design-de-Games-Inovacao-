﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityCore.Scene;
using UnityCore.Menu;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField]
    private string nomeDoMenu;
	[SerializeField]
	private string nomeDosCreditos;
    private SceneType nextScene;

    private void Start()
    {
        GameManager.Instance.ChecaFase();
        Debug.Log(GameManager.Instance.sceneAtual);
        GameManager.Instance.LoadGame();
    }

    public void ComecaJogo()
    {
        if(PlayerPrefsManager.Instance.prefsVariables.levelIndex > 0 || GameManager.historiaMode == false)
        {
            nomeDoMenu = "HUB";
            nextScene = SceneType.HUB;
            Debug.Log("ComecaJOGO[MenuPrincipal script]");
        }

        else
        {
            nomeDoMenu = "Historia";
            nextScene = SceneType.Historia;
        }
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Start", GetComponent<Transform>().position);
        LoadingManager.instance.LoadGame(nextScene);
        Debug.Log("ComecaJOGO[MenuPrincipal script]");
        /*SceneController.Instance.Load(nextScene, (_scene) => {
            Debug.Log("Scene [" + _scene + "] loaded from MenuPrincipal scrípt!");
        }, false, PageType.Loading);*/
        //SceneManager.LoadScene(nomeDoMenu);
    }

	public void Creditos()
	{
        FMODUnity.RuntimeManager.PlayOneShot("event:/HUD/Click", GetComponent<Transform>().position);
        SceneController.Instance.Load(SceneType.Creditos, (_scene) => {
            Debug.Log("Scene [" + _scene + "] loaded from MenuPrincipal scrípt!");
        }, false, PageType.Loading);
        //SceneManager.LoadScene(nomeDosCreditos);
    }
}
