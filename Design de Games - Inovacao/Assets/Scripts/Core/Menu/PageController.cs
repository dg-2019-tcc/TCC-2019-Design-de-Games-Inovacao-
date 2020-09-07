using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCore
{
    namespace Menu
    {
        public class PageController : MonoBehaviour
        {
            public static PageController instance;

            public bool debug;
            public PageType entryPage;
            public Page[] pages;
            [SerializeField]
            public static PageType pageAtiva;

            private Hashtable m_Pages;

            #region Unity Functions
            private void Awake()
            {
                if (!instance)
                {
                    instance = this;
                    m_Pages = new Hashtable();
                    RegisterAllPages();

                    if(entryPage != PageType.None)
                    {
                        if (entryPage == PageType.MobileHUD && GameManager.buildPC)
                        {
                            pageAtiva = PageType.None;
                            return;
                        }
                        else
                        {
                            TurnPageOn(entryPage);
                            pageAtiva = entryPage;
                        }
                    }

                    //DontDestroyOnLoad(gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }

            #endregion

            #region Public Functions
            public void TurnPageOn(PageType _type)
            {
                if (_type == PageType.None) return;
                if (!PageExists(_type)){LogWarning("You are trying to turn a page on ["+ _type+"] that has not been registered.");return;}

                Page _page = GetPage(_type);
                _page.gameObject.SetActive(true);
                _page.Animate(true);
                pageAtiva = _page.type;
            }

            public void TurnPageOff(PageType _off, PageType _on=PageType.None, bool _waitForExit=false)
            {
                if (_off == PageType.None) return;
                if (!PageExists(_off)){LogWarning("You are trying to turn a page on [" + _off + "] that has not been registered.");return;}

                Page _offPage = GetPage(_off);
                if (_offPage.gameObject.activeSelf)
                {
                    _offPage.Animate(false);
                }

                if (_on != PageType.None)
                {
                    Page _onPage = GetPage(_on);
                    if (_waitForExit)
                    {
                        StartCoroutine(WaitForPageExit(_onPage, _offPage));
                    }
                    else
                    {
                        TurnPageOn(_on);
                    }
                }
                else
                {
                    if (GameManager.needMobileHUD)
                    {
                        TurnPageOn(PageType.MobileHUD);
                    }
                }
            }

            public bool PageIsOn(PageType _type)
            {
                if (!PageExists(_type))
                {
                    LogWarning("You are trying to detect if a page is on [" + _type + "], but it has not been registered");
                    return false;
                }

                return GetPage(_type).isOn;
            }

            #endregion

            #region Private Functions
            private IEnumerator WaitForPageExit(Page _on, Page _off)
            {
                while(_off.targetState != Page.FLAG_None)
                {
                    yield return null;
                }

                TurnPageOn(_on.type);
            }

            private void RegisterAllPages()
            {
                foreach(Page _page in pages)
                {
                    RegisterPage(_page);
                }
            }

            private void RegisterPage(Page _page)
            {
                if (PageExists(_page.type)){LogWarning("You are trying to register a page on [" + _page.type + "] that has already been registered." + _page.gameObject.name);return;}

                m_Pages.Add(_page.type, _page);
                Log("Registered new page[" + _page.type + "]");
            } 

            private Page GetPage(PageType _type)
            {
                if (!PageExists(_type))
                {
                    LogWarning("You are trying to get a page [" + _type + "] that has not been registered.");
                    return null;
                }
                return (Page)m_Pages[_type];
            }

            private bool PageExists(PageType _type)
            {
                return m_Pages.Contains(_type);
            }

            private void Log(string _msg)
            {
                if (!debug) return;
                Debug.Log("[PageController]:" + _msg);
            }
            private void LogWarning(string _msg)
            {
                if (!debug) return;
                Debug.LogWarning("[PageController]:" + _msg);
            }
            #endregion
        }
    }
}
