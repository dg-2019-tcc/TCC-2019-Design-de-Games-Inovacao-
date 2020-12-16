using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCore
{
    namespace Menu
    {
        public class CanvasButtonChange : MonoBehaviour
        {
            public PageType pageType;
            public PageController pageController;
            [Header("typePageChange se é = 0 apenas ativa a page, se é = 1 só desativa a page, se é = 2 desativa a atual e atualiza a proxima, 3 é com animação")]
            public int typePageChange;

            private void Start()
            {
                pageController = FindObjectOfType<PageController>();
            }

            public void ChangePage()
            {
                if(pageController == null)
                {
                    pageController = FindObjectOfType<PageController>();
                }

                switch (typePageChange)
                {
                    case 0:
                        pageController.TurnPageOn(pageType);
                        break;

                    case 1:
                        pageController.TurnPageOff(pageType);
                        break;

                    case 2:
                        pageController.TurnPageOff(PageController.pageAtiva, pageType);
                        break;
                }
            }

        }
    }
}