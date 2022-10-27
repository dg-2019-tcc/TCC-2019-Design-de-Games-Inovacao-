using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOTweenMoveObjects : MonoBehaviour
{
    [SerializeField]
    private Vector3 _targetPosition = Vector3.zero;
    private Vector3 inicialPosition;

    [Range(0.1f, 10.0f), SerializeField]
    private float _moveDuration = 1.0f;

    [Range(-10.0f, 10.0f), SerializeField]
    private float _moveDistance = 0.5f;

    [SerializeField]
    private Ease moveEase = Ease.Linear;

    [SerializeField]
    private LoopType loopType = LoopType.Yoyo;

    [SerializeField]
    private DoTweenType doTweenType = DoTweenType.BounceCoin;

    private enum DoTweenType
    {
        MoveOneWay,
        MoveTwoWay,
        BounceCoin
    }

    #region Unity Function
    private void Start()
    {
        inicialPosition = transform.position;
        if (doTweenType == DoTweenType.BounceCoin) { BounceCoin();}
    }
    #endregion

    #region Public Functions

    #endregion

    #region Private Functions
    private void BounceCoin()
    {
        if (_targetPosition == Vector3.zero)
        {
            _targetPosition = inicialPosition;
            _targetPosition.y += _moveDistance;
        }
        transform.DOMove(_targetPosition, _moveDuration).SetEase(moveEase).SetLoops(-1, loopType);
    }
    #endregion

}
