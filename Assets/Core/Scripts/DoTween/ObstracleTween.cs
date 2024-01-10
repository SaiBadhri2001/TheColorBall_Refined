using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObstracleTween : MonoBehaviour
{
    [SerializeField] float scaleMultiply;
    [SerializeField] float animDuration;
    public Ease ease;
    void Start()
    {
        Tween tween = transform.DOScale(transform.localScale * scaleMultiply, animDuration).SetEase(ease).SetAutoKill(false).OnComplete(() => transform.DOSmoothRewind());
    }
    void Update()
    {

    }
}
