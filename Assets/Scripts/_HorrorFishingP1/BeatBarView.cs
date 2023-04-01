using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BeatBarView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer beatBarSprite;
    private float spriteAppearanceTimer = 2f;
    private bool isVisible = false;

    public void Animate_BeatBarAppearOrDisappear() {
        if (isVisible == false) {
            beatBarSprite.DOFade(255f, spriteAppearanceTimer);
            isVisible = true;
        }
        else {
            beatBarSprite.DOFade(0f, spriteAppearanceTimer);
            isVisible = false;
        }
    }
}
