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
    //public float lineFlyTimer = 0.1f;

    public void Animate_CastRod() {
        DOTween.Sequence()
            .Append(transform.DORotate(new Vector3(0, 0, 50), rodCastTimer, RotateMode.WorldAxisAdd))
                .SetEase(Ease.OutBack)
            .Append(transform.DORotate(new Vector3(0, 0, -50), rodCastTimer, RotateMode.WorldAxisAdd))
                .SetEase(Ease.InBack);
    }

    public void Animate_FishIsBiting() {
        transform.DOShakePosition(1f, new Vector3(0.15f, 0f, 0f), 10, 0f, false, false);
    }

    public void Animate_RodSpin(int rotationAngle, double beatDuration)
    {
        handle.DORotate(new Vector3(0, 0, rotationAngle), (float)beatDuration, RotateMode.WorldAxisAdd);
    }

    public void Animate_ShiftBeatColor(double beatDuration)
    {
        beatCircle.DOColor(Color.green, (float)beatDuration).SetEase(Ease.InCubic).OnComplete(() => beatCircle.color = Color.white);
    }
}
