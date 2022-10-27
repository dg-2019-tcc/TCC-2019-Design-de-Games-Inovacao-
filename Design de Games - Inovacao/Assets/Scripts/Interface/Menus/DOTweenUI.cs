using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DOTweenUI : MonoBehaviour
{
    public RectTransform rectTransform;
    public CanvasGroup canvas;
	public Canvas canvasRender;

    public GameObject coinCanvas;

    [SerializeField]
    private Vector2 targetPosition;
    [SerializeField]
    private Vector2 inicialPostion;

    [SerializeField]
    private Ease moveEase = Ease.Linear;
    [Range(0.1f, 10.0f), SerializeField]
    private float _moveDuration = 1.0f;

    public bool finishedTween;

    public AnimUIType anim;

    public enum AnimUIType
    {
        Alfa,
        Move,
        Coin
    }

    #region Unity Function

    private void OnEnable()
    {
        finishedTween = false;

        if (anim == AnimUIType.Move) { TweenIn(); }
        else if (anim == AnimUIType.Alfa) { ChangeAlfa(true); }
        else if (anim == AnimUIType.Coin) { TweenInCoin(); }
    }

    private void OnDisable()
    {
        finishedTween = false;

        if (anim == AnimUIType.Move) { TweenOut(); }
        else if (anim == AnimUIType.Alfa) { ChangeAlfa(false); }

    }

    #endregion

    #region Public Functions

    public void ChangeAlfa(bool turnOn)
    {
        if (canvas == null) { canvas = GetComponent<CanvasGroup>(); }
        if (turnOn)
        {
            canvas.DOFade(1f, _moveDuration).SetEase(moveEase);
        }
        else
        {
            canvas.DOFade(0f, _moveDuration).SetEase(moveEase).OnComplete(TweenOutCallback());
        }
    }

    public void TweenInCoin()
    {
        DOTween.Sequence().Append(rectTransform.DOAnchorPos(targetPosition, _moveDuration).SetEase(moveEase)).OnComplete(() => { Destroy(coinCanvas); });
    }


    public void TweenIn()
    {
        rectTransform.DOAnchorPos(Vector2.zero, _moveDuration).SetEase(moveEase);
    }

    public void TweenOut()
    {
        rectTransform.DOAnchorPos(inicialPostion, _moveDuration).SetEase(moveEase).OnComplete(TweenOutCallback());
    }

    #endregion

    #region Private Functions

    TweenCallback TweenOutCoinCallback()
    {
        //rectTransform.DOAnchorPos(inicialPostion, _moveDuration).SetEase(moveEase);
        finishedTween = true;
        //gameObject.SetActive(false);
        return null;
    }

    TweenCallback TweenOutCallback()
    {
        rectTransform.DOAnchorPos(inicialPostion, _moveDuration).SetEase(moveEase);
        finishedTween = true;

        return null;
    }

    #endregion
}
