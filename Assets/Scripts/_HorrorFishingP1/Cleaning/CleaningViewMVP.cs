using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningViewMVP : MonoBehaviour
{
    [SerializeField] private SpriteRenderer currentCleaningSprite;
    [SerializeField] private Sprite preCleaningSprite;
    [SerializeField] private Sprite postCleaningSprite;

    [SerializeField] private AudioSource cleaningAudioSource;

    public void Animate_UnhookFish() {
        currentCleaningSprite.sprite = postCleaningSprite;
    }

    public void ResetCleaningView() {
        currentCleaningSprite.sprite = preCleaningSprite;
    }

    public void Play_UnhookSFX() {
        cleaningAudioSource.PlayOneShot(cleaningAudioSource.clip);
    }
}
