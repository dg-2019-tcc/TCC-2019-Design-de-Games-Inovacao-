using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCore
{
    namespace ScriptableObject
    {
        public class ScriptableObjectManager : MonoBehaviour
        {
            public BoolVariable buildPC;
            public BoolVariable buildMobile;
            public BoolVariable buildFinal;
            public BoolVariable resetaPlayerPrefs;
            public BoolVariable pularModoHistoria;
            public BoolVariable escolheFase;
            public FloatVariable faseEscolhida;

            #region Unity Function

            #region Singleton
            private static ScriptableObjectManager _instance;

            public static ScriptableObjectManager Instance
            {
                get
                {
                    if (_instance == null)
                    {
                        _instance = FindObjectOfType<ScriptableObjectManager>();
                        if (_instance == null)
                        {
                            GameObject go = new GameObject();
                            go.name = typeof(ScriptableObjectManager).Name;
                            _instance = go.AddComponent<ScriptableObjectManager>();
                            DontDestroyOnLoad(go);
                        }
                    }
                    return _instance;
                }
            }

            private void Awake()
            {
                if (_instance == null)
                {
                    _instance = this;
                    DontDestroyOnLoad(this.gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            #endregion

            #endregion

            #region Public Functions

            #endregion

            #region Private Functions

            void ResetScriptableObjects()
            {
                buildPC.Value = false;
                buildMobile.Value = false;
                buildFinal.Value = false;
                resetaPlayerPrefs.Value = false;
                pularModoHistoria.Value = false;
                escolheFase.Value = false;
                faseEscolhida.Value = 0;
            }
            #endregion
        }
    }
}
