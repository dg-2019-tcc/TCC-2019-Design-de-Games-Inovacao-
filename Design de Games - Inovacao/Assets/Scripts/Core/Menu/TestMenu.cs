using UnityEngine;

namespace UnityCore
{
    namespace Menu
    {
        public class TestMenu : MonoBehaviour
        {
            public PageController pageController;
#if UNITY_EDITOR
            private void Update()
            {
                if (Input.GetKeyUp(KeyCode.F))
                {
                    Debug.Log("Apertou F");
                    pageController.TurnPageOn(PageType.Options);
                }
                if (Input.GetKeyUp(KeyCode.G))
                {
                    pageController.TurnPageOff(PageType.Options);
                }

                if (Input.GetKeyUp(KeyCode.H))
                {
                    pageController.TurnPageOff(PageType.Loading, PageType.Menu);
                }
                if (Input.GetKeyUp(KeyCode.J))
                {
                    pageController.TurnPageOff(PageType.Menu, PageType.Options, true);
                }
            }
#endif
        }
    }
}
