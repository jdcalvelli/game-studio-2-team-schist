using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FishingRodView : MonoBehaviour
{
    
    // get sub elements
    [SerializeField] private Transform handle;
    [SerializeField] private SpriteRenderer beatCircle;
    
    public float rodCastTimer = 1f;
    public float lineFlyTimer = 0.1f;

    public void Animate_CastRod() {
        Sequence seq = DOTween.Sequence();
        Vector3 originalRodPosition = transform.position;
        Vector3 originalScale = transform.localScale;


        seq.Append(transform.DOMove(new Vector3(transform.position.x - 1f, transform.position.y + 1.5f, transform.position.z), rodCastTimer));
        seq.Join(transform.DOScale(new Vector3(1.5f, 1.5f, 1), rodCastTimer));
        seq.Join(transform.DORotate(new Vector3(0,0, 30), rodCastTimer).OnComplete(() => {
            transform.DOMove(originalRodPosition, lineFlyTimer);
            transform.DOScale(originalScale, lineFlyTimer);
            transform.DORotate(Vector3.zero, lineFlyTimer);
        }));

    }

    public void Animate_FishIsBiting() {
        transform.DOShakePosition(1f, new Vector3(0.15f, 0f, 0f), 10, 0f, false, false);
    }

    public void Animate_RodSpin(double beatDuration)
    {
        handle.DORotate(new Vector3(0, 0, 360), (float)beatDuration, RotateMode.WorldAxisAdd);
    }

    public void Animate_ShiftBeatColor(double beatDuration)
    {
        beatCircle.DOColor(Color.green, (float)beatDuration).SetEase(Ease.InCubic).OnComplete(() => beatCircle.color = Color.white);
    }
}
