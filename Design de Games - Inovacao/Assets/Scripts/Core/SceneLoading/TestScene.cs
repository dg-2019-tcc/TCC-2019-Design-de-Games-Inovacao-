using UnityEngine;
using UnityCore.Menu;

namespace UnityCore
{
    namespace Scene
    {
        public class TestScene : MonoBehaviour
        {
            private void Awake()
            {
                DontDestroyOnLoad(gameObject);
            }

            #region Unity Function
#if UNITY_EDITOR
            private void Update()
            {
                if (Input.GetKeyUp(KeyCode.M))
                {
                    //sceneController = FindObjectOfType<SceneController>();
                    SceneController.Instance.Load(SceneType.HUB, (_scene) => {
                        Debug.Log("Scene [" + _scene + "] loaded from test scrípt!");
                    }, false, PageType.Loading);
                }

                if (Input.GetKeyUp(KeyCode.G))
                {
                    SceneController.Instance.Load(SceneType.MenuPrincipal);
                }
            }

#endif

            #endregion
        }
    }
}
