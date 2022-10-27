using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

namespace UnityCore
{
    namespace Menu
    {
        public class DOTweenAnim : MonoBehaviour
        {
            private RectTransform rectTransform;
            public UIAnimType animType;
            private Vector2 animDir;

            public Tweener tweener;

            #region Public Funcitons
            public void PageIn()
            {
                if(rectTransform == null) { rectTransform = GetComponent<RectTransform>(); }
                rectTransform.DOAnchorPos(Vector2.zero, 0.25f);
            }
            public void PageOut()
            {
                if (rectTransform == null) { rectTransform = GetComponent<RectTransform>(); }
                AnimDirection();
                rectTransform.DOAnchorPos(animDir, 0.25f);
            }
            #endregion

            #region Private Functions
            private void AnimDirection()
            {
                switch (animType)
                {
                    case UIAnimType.Up:
                        animDir = new Vector2(0, 1200);
                        break;

                    case UIAnimType.Down:
                        animDir = new Vector2(0, -1200);
                        break;

                    case UIAnimType.Left:
                        animDir = new Vector2(2000, 0);
                        break;

                    case UIAnimType.Right:
                        animDir = new Vector2(-2000, 0);
                        break;
                }
            }
            #endregion
        }
    }
}
