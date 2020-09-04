using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DOTweenUI : MonoBehaviour
{
    public RectTransform rectTransform;

    [SerializeField]
    private Vector2 targetPosition;
    [SerializeField]
    private Vector2 inicialPostion;

    [SerializeField]
    private Ease moveEase = Ease.Linear;
    [Range(0.1f, 10.0f), SerializeField]
    private float _moveDuration = 1.0f;


    private void OnEnable()
    {
        TweenIn();
    }

    public void TweenIn()
    {
        rectTransform.DOAnchorPos(Vector2.zero, _moveDuration).SetEase(moveEase);
        Debug.Log("TweenIn()");
    }

    public void TweenOut()
    {
        rectTransform.DOAnchorPos(inicialPostion, _moveDuration).SetEase(moveEase);

        Debug.Log("Acabou tween");
    }
    public void DesativaObj()
    {
        gameObject.SetActive(false);
    }
}
