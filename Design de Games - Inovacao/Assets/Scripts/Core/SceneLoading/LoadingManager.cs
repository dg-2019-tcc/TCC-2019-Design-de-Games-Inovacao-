using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using UnityCore.Scene;
using UnityCore.Menu;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager instance;
    public GameObject loadingScreen;
    public bool isLoading;

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

        if (tweenUI == null)
        {
            //tweenUI = GetComponentInChildren<DOTweenUI>();
        }

        if(tweenCanvas == null)
        {
            tweenCanvas = GetComponentInChildren<DOTweenUI>();
        }
    }

    #endregion

    #region Public Functions
    public void LoadNewScene(SceneType nextScene, SceneType oldScene, bool isOnline)
    {
        loadingScreen.SetActive(true);
        //tweenUI.TweenIn();
        GameManager.isPaused = true;
        GameManager.sceneAtual = nextScene;
        GameManager.Instance.sceneOld = oldScene;
        if (!isOnline){ Invoke("InitOfflineScene", 1f);}
        else { StartCoroutine(InitOnlineScene(nextScene, oldScene)); }
    }
    #endregion

    #region Private Functions
    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    private IEnumerator InitOnlineScene(SceneType nextScene, SceneType oldScene)
    {
        Debug.Log("InitOnlineScene");
        PhotonNetwork.LoadLevel((int)GameManager.sceneAtual);
        loadingScreen.SetActive(true);
        while (PhotonNetwork.LevelLoadingProgress < 1) { yield return null; }
        while (SceneInitializer.current.isDone == false) { yield return null; }
        //loadingScreen.SetActive(false);
        tweenUI.TweenOut();
        while (tweenUI.finishedTween == false) { yield return null; }
        tweenCanvas.ChangeAlfa(false);
        loadingScreen.SetActive(false);

        Debug.Log("[LoadingManager] Loaded Online Scene");
        GameManager.isPaused = false;
    }

    private void InitOfflineScene()
    {
        //scenesLoading.Add(SceneManager.UnloadSceneAsync((int)oldScene));
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

        tweenUI.TweenOut();
        tweenCanvas.ChangeAlfa(false);
        while (tweenUI.finishedTween == false) { yield return null; }
        while (tweenCanvas.finishedTween == false) { yield return null; }
        loadingScreen.SetActive(false);
        GameManager.isPaused = false;
        Debug.Log("[LoadingManager] Loaded "+ nextScene+ " Scene Sucess");
    }
    #endregion
}
