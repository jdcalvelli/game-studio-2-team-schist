using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitingViewMVP : MonoBehaviour
{
    [SerializeField] private SpriteRenderer currentBaitSprite;
    [SerializeField] private Sprite preBaitSprite;
    [SerializeField] private Sprite postBaitSprite;

    public void Animate_BaitHook() {
        currentBaitSprite.sprite = postBaitSprite;
    }

    public void ResetBaitView() {
        currentBaitSprite.sprite = preBaitSprite;
    }
}
