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
    }

    #endregion

    #region Public Functions
    public void LoadNewScene(SceneType nextScene, SceneType oldScene, bool isOnline)
    {
        loadingScreen.SetActive(true);

        GameManager.isPaused = true;
        GameManager.Instance.sceneAtual = nextScene;
        GameManager.Instance.sceneOld = oldScene;

        if (!isOnline){ InitOfflineScene(nextScene, oldScene);}
        else { StartCoroutine(InitOnlineScene(nextScene, oldScene)); }
    }
    #endregion

    #region Private Functions
    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    private IEnumerator InitOnlineScene(SceneType nextScene, SceneType oldScene)
    {
        Debug.Log("InitOnlineScene");
        PhotonNetwork.LoadLevel((int)nextScene);
        loadingScreen.SetActive(true);
        while (PhotonNetwork.LevelLoadingProgress < 1) { yield return null; }
        while (SceneInitializer.current.isDone == false) { yield return null; }
        Debug.Log("[LoadingManager] Loaded Online Scene");
        GameManager.isPaused = false;
        loadingScreen.SetActive(false);
    }

    private void InitOfflineScene(SceneType nextScene, SceneType oldScene)
    {
        //scenesLoading.Add(SceneManager.UnloadSceneAsync((int)oldScene));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)nextScene, LoadSceneMode.Single));
        StartCoroutine(GetSceneLoadProgress(nextScene));
    }

    private IEnumerator GetSceneLoadProgress(SceneType nextScene)
    {
        for (int i = 0; i <scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone) { yield return null; }
        }

        while (SceneInitializer.current.isDone == false) { yield return null; }

        loadingScreen.SetActive(false);
        GameManager.isPaused = false;
        Debug.Log("[LoadingManager] Loaded "+ nextScene+ " Scene Sucess");
    }
    #endregion
}
