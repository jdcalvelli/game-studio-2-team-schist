using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FishingRodView : MonoBehaviour
{
    private float rodCastTimer = 1f;
    private float lineFlyTimer = 0.1f;

    public void Animate_CastRod() {
        Vector3 originalRodPosition = transform.position;
        Vector3 originalScale = transform.localScale;

        transform.DOMove(new Vector3(transform.position.x + 1f, transform.position.y + 1.5f, transform.position.z), rodCastTimer);
        transform.DOScale(new Vector3(1.5f, 1.5f, 1), rodCastTimer);
        transform.DORotate(new Vector3(0,0,-30), rodCastTimer).OnComplete(() => {
            transform.DOMove(originalRodPosition, lineFlyTimer);
            transform.DOScale(originalScale, lineFlyTimer);
            transform.DORotate(Vector3.zero, lineFlyTimer);
        });
    }

    public void Animate_FishIsBiting() {
        transform.DOShakePosition(1f, new Vector3(0.15f, 0f, 0f), 10, 0f, false, false);
    }
}
