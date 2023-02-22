using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NoteView : MonoBehaviour
{
    private SpriteRenderer noteSprite;
    private int originalRotation;

    void Awake() {
        noteSprite = gameObject.GetComponent<SpriteRenderer>();
    }
    public void Animate_NoteAppear() {
        noteSprite.DOFade(255, 0.5f);

        transform.DORotate(new Vector3(0, 0, -15), 0.75f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    public void Animate_NoteDisappear() {
        noteSprite.DOFade(0, 0.5f);
    }

    public void Animate_NoteHit() {
        Vector3 originalScale = transform.localScale;
        transform.DOScale(new Vector3(1.25f, 1.25f, 1f), 0.1f).OnComplete(() => {
            transform.DOScale(originalScale, 0.1f);
        });
    }

    public void Animate_NoteMiss() {
        Color originalColor = noteSprite.color;
        Vector3 originalScale = transform.localScale;

        transform.DOScale(new Vector3(0.75f, 0.75f, 1f), 0.25f);
        transform.DOShakePosition(0.25f, new Vector3(0.1f, 0f, 0f), 15, 0f, false, false);
        noteSprite.DOColor(Color.grey, 0.25f).OnComplete(() => {
            noteSprite.DOColor(originalColor, 0.25f);
            transform.DOScale(originalScale, 0.25f);
        });
    }
}
