using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityCore.Menu;

namespace UnityCore
{
    namespace Scene
    {
        public class SceneController : MonoBehaviour
        {
            public delegate void SceneLoadDelegate(SceneType _scene);

            public static SceneController instance;

            public bool debug = true;

            private PageController m_Menu;
            private SceneType m_TargetScene;
            private PageType m_LoadingPage;
            private SceneLoadDelegate m_SceneLoadDelegate;
            private bool m_SceneIsLoading;

            private PageController menu
            {
                get
                {
                    if(m_Menu == null)
                    {
                        m_Menu = PageController.instance;
                    }

                    if(m_Menu == null)
                    {
                        LogWarning("You are trying to acess the PageController, but no instance was found.");
                    }
                    return m_Menu;
                }
            }

            private string currentSceneName
            {
                get
                {
                    return SceneManager.GetActiveScene().name;
                }
            }

            #region Unity Functions

            #region Singleton

            public static SceneController Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = FindObjectOfType<SceneController>();
                        if (instance == null)
                        {
                            GameObject go = new GameObject();
                            go.name = typeof(SceneController).Name;
                            instance = go.AddComponent<SceneController>();
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
                    Configure();
                    DontDestroyOnLoad(this.gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }

            #endregion

            private void OnDisable()
            {
                Dispose();
            }
            #endregion

            #region Public Functions
            public void Load(SceneType _scene, SceneLoadDelegate _sceneLoadDelegate=null,bool _reload=false, PageType _loadingPage = PageType.None)
            {
                if(_loadingPage != PageType.None && !menu){return;}

                if(!SceneCanBeLoaded(_scene, _reload)) { return; }

                m_SceneIsLoading = true;
                m_TargetScene = _scene;
                m_LoadingPage = _loadingPage;
                m_SceneLoadDelegate = _sceneLoadDelegate;
                StartCoroutine("LoadScene");
            }
            #endregion

            #region Private Functions
            private void Configure()
            {
                instance = this;
                SceneManager.sceneLoaded += OnSceneLoaded;
            }

            private void Dispose()
            {
                SceneManager.sceneLoaded -= OnSceneLoaded;
            }

            private async void OnSceneLoaded(UnityEngine.SceneManagement.Scene _scene, LoadSceneMode _mode)
            {
                if(m_TargetScene == SceneType.None) { return; }

                SceneType _sceneType = StringToSceneType(_scene.name);

                if(m_TargetScene != _sceneType) { return; }

                if(m_SceneLoadDelegate != null)
                {
                    try
                    {
                        m_SceneLoadDelegate(_sceneType);
                    }
                    catch (System.Exception)
                    {
                        LogWarning("Unable to respond with sceneLoadDelegate after scene [" + _sceneType + "] loaded.");
                    }
                }

                if(m_LoadingPage != null)
                {
                    //await Task.Delay(1000);
                    menu.TurnPageOff(m_LoadingPage);
                }

                m_SceneIsLoading = false;
            }

            private IEnumerator LoadScene()
            {
                if(m_LoadingPage != PageType.None)
                {
                    menu.TurnPageOn(m_LoadingPage);
                    while (!menu.PageIsOn(m_LoadingPage))
                    {
                        yield return null;
                    }
                }

                    string _targetSceneName = SceneTypeToString(m_TargetScene);
                    SceneManager.LoadScene(_targetSceneName);
            }

            private bool SceneCanBeLoaded(SceneType _scene, bool _reload)
            {
                string _targetSceneName = SceneTypeToString(_scene);
                if(currentSceneName == _targetSceneName && !_reload)
                {
                    LogWarning("You are trying to load a scene [" + _scene + "] which is already active.");
                    return false;
                }
                else if(currentSceneName == string.Empty)
                {
                    LogWarning("The scene you are trying to load [" + _scene + "] is not valide.");
                    return false;
                }
                else if (m_SceneIsLoading)
                {
                    LogWarning("Unable to load scene [" + _scene + "]. Another scene["+m_TargetScene+"] is already loading.");
                    return false;
                }

                return true;
            }

            private string SceneTypeToString(SceneType _scene)
            {
                switch (_scene)
                {
                    case SceneType.HUB: return "HUB";
                    case SceneType.MenuPrincipal: return "MenuPrincipal";
                    case SceneType.Creditos: return "Creditos";
                    case SceneType.Historia: return "Historia";
                    default:
                        LogWarning("Scene [" + _scene + "] does not contain a string for a valid scene.");
                        return string.Empty;
                }
            }

            private SceneType StringToSceneType(string _scene)
            {
                switch (_scene)
                {
                    case "HUB": return SceneType.HUB;
                    case "MenuPrincipal": return SceneType.MenuPrincipal;
                    default:
                        LogWarning("Scene [" + _scene + "] does not contain a type for a valid scene.");
                        return SceneType.None;
                }
            }

            private void Log(string _msg)
            {
                if (!debug) return;
                Debug.Log("[Scene Controller]: " + _msg);
            }

            private void LogWarning(string _msg)
            {
                if (!debug) return;
                Debug.LogWarning("[Scene Controller]: " + _msg);
            }
            #endregion
        }
    }
}
