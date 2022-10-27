using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using UnityCore.Scene;
using UnityCore.Menu;
using System;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager instance;
    public GameObject loadingScreen;
    public static bool isLoading;

    public DOTweenUI tweenUI;
    public DOTweenUI tweenCanvas;

    #region Singleton

    public static LoadingManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LoadingManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(LoadingManager).Name;
                    instance = go.AddComponent<LoadingManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if(tweenCanvas == null){ tweenCanvas = GetComponentInChildren<DOTweenUI>();}
    }
    #endregion

    #region Public Functions
    public void LoadNewScene(SceneType nextScene, SceneType oldScene, bool isOnline)
    {
        if (!isLoading)
        {
            if (nextScene != SceneType.TelaVitoria) { GameManager.ganhou = false; GameManager.perdeu = false; GameManager.acabouFase = false; }
            loadingScreen.SetActive(true);
            GameManager.isPaused = true;
            GameManager.sceneAtual = nextScene;
            GameManager.Instance.scene = GameManager.sceneAtual;
            GameManager.Instance.sceneOld = oldScene;
            if (!isOnline) { Invoke("InitOfflineScene", 1f); }
            else { StartCoroutine(InitOnlineScene(nextScene, oldScene)); }
            isLoading = true;
        }
        Debug.Log("[LoadingManager] Load New Scene");
    }
    #endregion

    #region Private Functions
    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    private IEnumerator InitOnlineScene(SceneType nextScene, SceneType oldScene)
    {
        isLoading = false;
        PhotonNetwork.LoadLevel((int)GameManager.sceneAtual);

        loadingScreen.SetActive(true);

        while (PhotonNetwork.LevelLoadingProgress < 1) { yield return null; }
        while (SceneInitializer.current.isDone == false) { yield return null; }

        GameManager.Instance.ChecaFase();

        tweenUI.TweenOut();
        while (tweenUI.finishedTween == false) { yield return null; }
        tweenCanvas.ChangeAlfa(false);
        loadingScreen.SetActive(false);

        GameManager.isPaused = false;
    }

    private void InitOfflineScene()
    {
        isLoading = false;
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)GameManager.sceneAtual, LoadSceneMode.Single));
        StartCoroutine(GetSceneLoadProgress(GameManager.sceneAtual));
    }

    private IEnumerator GetSceneLoadProgress(SceneType nextScene)
    {
        for (int i = 0; i <scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone) { yield return null; }
        }

        while (SceneInitializer.current.isDone == false) { yield return null; }

        GameManager.Instance.ChecaFase();

        tweenUI.TweenOut();
        tweenCanvas.ChangeAlfa(false);
        while (tweenUI.finishedTween == false) { yield return null; }
        while (tweenCanvas.finishedTween == false) { yield return null; }

        loadingScreen.SetActive(false);
        GameManager.isPaused = false;
    }
    #endregion
}
