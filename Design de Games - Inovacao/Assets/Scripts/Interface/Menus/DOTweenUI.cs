using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DOTweenUI : MonoBehaviour
{
    public RectTransform rectTransform;
    public CanvasGroup canvas;

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
        Move
    }


    private void OnEnable()
    {
        finishedTween = false;
        if (anim == AnimUIType.Move) { TweenIn(); }
        else if(anim == AnimUIType.Alfa) { ChangeAlfa(true); }
    }

    private void OnDisable()
    {
        finishedTween = false;
    }

    public void ChangeAlfa(bool turnOn)
    {
        if(canvas == null) { canvas = GetComponent<CanvasGroup>(); }
        if (turnOn)
        {
            canvas.DOFade(1f, _moveDuration).SetEase(moveEase);
        }
        else
        {
            canvas.DOFade(0f, _moveDuration).SetEase(moveEase).OnComplete(TweenOutCallback());
        }
    }

    public void TweenIn()
    {
        rectTransform.DOAnchorPos(Vector2.zero, _moveDuration).SetEase(moveEase);
        Debug.Log("TweenIn()");
    }

    public void TweenOut()
    {
        rectTransform.DOAnchorPos(inicialPostion, _moveDuration).SetEase(moveEase).OnComplete(TweenOutCallback());

        Debug.Log("Acabou tween");
    }


    TweenCallback TweenOutCallback()
    {
        finishedTween = true;
        Debug.Log("FinishCallback tween" + finishedTween);
        return null;
    }
}
