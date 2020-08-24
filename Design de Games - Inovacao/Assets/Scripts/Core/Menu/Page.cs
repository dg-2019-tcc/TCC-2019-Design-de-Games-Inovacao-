using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

namespace UnityCore
{
    namespace Menu
    {
        public class Page : MonoBehaviour
        {
            public bool debug;

            public static readonly string FLAG_ON = "On";
            public static readonly string FLAG_OFF = "Off";
            public static readonly string FLAG_None = "None";

            public PageType type;
            public bool useAnimation;
            public string targetState { get; private set;}

            //private Animator m_Animator;
            public UIAnimType uiAnimType;
            private RectTransform rectTransform;
            private Vector2 animDir;
            private bool m_IsOn;

            public bool isOn
            {
                get
                {
                    return m_IsOn;
                }
                private set
                {
                    m_IsOn = value;
                }
            }

            #region Unity Functions
            private void OnEnable()
            {
                AnimDirection();
                CheckAnimatorIntegrity();
            }
            #endregion

            #region Public Functions
            public void Animate(bool _on)
            {
                if (useAnimation)
                {
                    //m_Animator.SetBool("on", _on);
                    if (_on)
                    {
                        rectTransform.DOAnchorPos(Vector2.zero, 0.25f);
                    }
                    else
                    {
                        rectTransform.DOAnchorPos(animDir, 0.25f);
                        Debug.Log("nao ta _on");
                    }

                    StopCoroutine("AwaitAnimation");
                    StartCoroutine("AwaitAnimation", _on);
                }
                else
                {
                    if (!_on)
                    {
                        isOn = false;
                        gameObject.SetActive(false);
                    }
                    else
                    {
                        isOn = true;
                    }
                }
            }
            #endregion

            #region Private Functions
            private void AnimDirection()
            {
                switch (uiAnimType)
                {
                    case UIAnimType.Up:
                        animDir = new Vector2(0, -1080);
                        break;

                    case UIAnimType.Down:
                        animDir = new Vector2(0, 1080);
                        break;

                    case UIAnimType.Left:
                        animDir = new Vector2(-2500, 0);
                        break;

                    case UIAnimType.Right:
                        animDir = new Vector2(2500, 0);
                        break;
                }
            }

            private IEnumerator AwaitAnimation(bool _on)
            {
                targetState = _on ? FLAG_ON : FLAG_OFF;

                yield return new WaitForSeconds(0.25f);
                /*// wait for the animator to reach the target state
                while (!m_Animator.GetCurrentAnimatorStateInfo(0).IsName(targetState))
                {
                    yield return null;
                }


                // wait for the animator to finish animating
                while(m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
                {
                    yield return null;
                }*/

                targetState = FLAG_None;

                Log("Page [" + type + "] finished transitioning to " + (_on ? "on" : "off"));

                if (!_on)
                {
                    //rectTransform.DOAnchorPos(posIni, 0.25f);
                    isOn = false;
                    gameObject.SetActive(false);
                }
                else
                {
                    isOn = true;
                }
            }

            private void CheckAnimatorIntegrity()
            {
                if (useAnimation)
                {
                    rectTransform = GetComponent<RectTransform>();
                    if (!rectTransform)
                    {
                        LogWarning("A page nao tem rect Transform");
                    }
                   /* m_Animator = GetComponent<Animator>();
                    if (!m_Animator)
                    {
                        LogWarning("You opted to animate a page [" + type + "], but no Animator component existis on the object.");
                    }*/
                }
            }

            private void Log(string _msg)
            {
                if (!debug) return;
                Debug.Log("[Page]:" + _msg);
            }
            private void LogWarning(string _msg)
            {
                if (!debug) return;
                Debug.Log("[Page]:" + _msg);
            }
            #endregion


        }
    }
}
