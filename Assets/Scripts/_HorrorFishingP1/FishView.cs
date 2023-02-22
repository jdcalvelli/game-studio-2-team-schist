using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FishView : MonoBehaviour
{
    private SpriteRenderer fishSprite;

    void Awake() {
        fishSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public void Animate_FishCaught() {
        Vector3 originalScale = transform.localScale;

        fishSprite.DOFade(255, 1f);

        transform.DOScale(new Vector3(2.5f, 2.5f, 0f), 0.25f).OnComplete(() => {
            transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
            fishSprite.DOFade(0, 2.5f);
        });
    }
}
