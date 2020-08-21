using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCore
{
    namespace Menu
    {
        public class CanvasManager : MonoBehaviour
        {
            public PageController pageController;

            public void Start()
            {
                pageController = GetComponent<PageController>();
            }
        }
    }
}
