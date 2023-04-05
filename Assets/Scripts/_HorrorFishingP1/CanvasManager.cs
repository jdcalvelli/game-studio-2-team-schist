using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public enum textPositions {
        bottomLeft,
        bottomCenter,
        topLeft
    }

    [SerializeField] private Image borderImage;

    [SerializeField] private Sprite fishingUI;
    [SerializeField] private Sprite baitingAndCleaningUI;

    [SerializeField] private TextMeshProUGUI bot_left_text;
    [SerializeField] private TextMeshProUGUI bot_center_text;
    [SerializeField] private TextMeshProUGUI top_left_text;

    public void SetFishingUI() {
        borderImage.sprite = fishingUI;
    }

    public void SetBaitingCleaningUI() {
        borderImage.sprite = baitingAndCleaningUI;
    }

    public void SetText(textPositions textPosition, string newText) {
        GrabReferencedText(textPosition).text = newText;
    }

    public void ActivateText(textPositions textPosition) {
        GrabReferencedText(textPosition).gameObject.SetActive(true);
    }

    public void DeactivateText(textPositions textPosition) {
        GrabReferencedText(textPosition).gameObject.SetActive(false);
    }

    private TextMeshProUGUI GrabReferencedText(textPositions textPosition) {
        TextMeshProUGUI referencedText = null;
        switch (textPosition) {
            case textPositions.bottomLeft:
                referencedText = bot_left_text;
                break;
            case textPositions.bottomCenter:
                referencedText = bot_center_text;
                break;
            case textPositions.topLeft:
                referencedText = top_left_text;
                break;
        }
        return referencedText;
    }
}
