using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BeatBarView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer beatBarSprite;
    private float spriteAppearanceTimer = 1.5f;
    private bool isVisible = false;

    public void Animate_BeatBarAppearOrDisappear() {
        if (isVisible == false) {
            beatBarSprite.DOFade(255, 2.5f);
            isVisible = true;
        }
        else {
            beatBarSprite.DOFade(0, 2.5f);
            isVisible = false;
        }
    }
}
