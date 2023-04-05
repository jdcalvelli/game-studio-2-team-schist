using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NoteView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer noteSprite;

    [SerializeField] private GameObject beatBar;
    [SerializeField] private SpriteRenderer beatBarSprite;

    // Waddling rotation animation for note gradually weakens, I can't figure out why
    public void Animate_NoteAppear() {
        noteSprite.DOFade(255, 0.5f);
        //transform.position = new Vector3(beatBar.transform.position.x + beatBarSprite.sprite.rect.width, beatBar.transform.position.y + (beatBarSprite.sprite.rect.height / 2f), 0f);
        //transform.position = new Vector3(8f, 5f, 0f);
        transform.position = new Vector3(beatBar.transform.position.x, beatBar.transform.position.y + 4.5f, 0f);
        transform.DORotate(new Vector3(0f, 0f, -15f), 0.75f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    public void Animate_NoteDisappear() {
        transform.localRotation = Quaternion.identity;
        noteSprite.DOFade(0, 0.5f);
    }

    public void Animate_NoteHit() {
        //Vector3 originalScale = transform.localScale;
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOScale(new Vector3(2f, 2f, 1f), 0.1f));
        seq.Join(noteSprite.DOFade(0, 0.5f));
    }

    public void Animate_NoteMiss() {
        Color originalColor = noteSprite.color;
        Vector3 originalScale = transform.localScale;

        transform.DOScale(new Vector3(0.90f, 0.90f, 1f), 0.25f);
        transform.DOShakePosition(0.25f, new Vector3(0.1f, 0f, 0f), 15, 0f, false, false);
        noteSprite.DOColor(Color.grey, 0.25f).OnComplete(() => {
            noteSprite.DOColor(originalColor, 0.25f);
            transform.DOScale(originalScale, 0.25f);
        });
    }

    public IEnumerator Animate_MoveNoteAlongBar(float timeBetweenBeats) {
        // move note along bar
        Animate_NoteAppear();
        //transform.DOMove(new Vector3(beatBar.transform.position.x + (noteSprite.sprite.rect.width / 2f), beatBar.transform.position.y + (noteSprite.sprite.rect.height / 2f), 0f), timeBetweenBeats);
        transform.DOMove(new Vector3(beatBar.transform.position.x, beatBar.transform.position.y - 4.5f, 0f), timeBetweenBeats).SetEase(Ease.Linear);
        //transform.DOMove(Vector3.down, timeBetweenBeats).SetEase(Ease.Linear);
        yield return new WaitForSeconds(timeBetweenBeats);
        Animate_NoteDisappear();
    }
}
