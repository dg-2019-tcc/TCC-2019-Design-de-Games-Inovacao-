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
    [SerializeField]
    private SceneType sceneAtiva;

    private void Awake()
    {
        instance = this;

        SceneManager.LoadSceneAsync((int)SceneType.FirstScene, LoadSceneMode.Additive);
    }

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    public void LoadGame(SceneType nextScene)
    {
        PageController.instance.TurnPageOff(PageController.pageAtiva, PageType.Loading);
        loadingScreen.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)sceneAtiva));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)nextScene, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    public IEnumerator GetSceneLoadProgress()
    {
        for(int i = 0; i <scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone) { yield return null; }
        }
        while(HubDelaySpawner.current.isDone == false) { yield return null; }
        loadingScreen.SetActive(false);
        //PageController.instance.TurnPageOff(PageType.Loading);
        Debug.Log("LoadingManager");
    }
}
