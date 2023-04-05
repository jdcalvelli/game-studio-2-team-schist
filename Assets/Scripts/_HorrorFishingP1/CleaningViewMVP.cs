using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningViewMVP : MonoBehaviour
{
    [SerializeField] private SpriteRenderer currentCleaningSprite;
    [SerializeField] private Sprite preCleaningSprite;
    [SerializeField] private Sprite postCleaningSprite;

    public void Animate_UnhookFish() {
        currentCleaningSprite.sprite = postCleaningSprite;
    }

    public void ResetCleaningView() {
        currentCleaningSprite.sprite = preCleaningSprite;
    }
}
