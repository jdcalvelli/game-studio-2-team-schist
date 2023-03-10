using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThoughtManager : MonoBehaviour
{

    [SerializeField] private GameObject A;
    [SerializeField] private GameObject B;
    [SerializeField] private GameObject C;
    [SerializeField] private GameObject I_A;
    [SerializeField] private GameObject I_B;

    private float thoughtTimer = 4f;
    private IEnumerator thoughtDisappear;

    private void Update() {
        if (HF_GameManager.hiddenScore == 10) {
            HandleThoughtBubble(A);
        }
        else if (HF_GameManager.hiddenScore == 20) {
            HandleThoughtBubble(B);
        }
        else if (HF_GameManager.hiddenScore == 30) {
            HandleThoughtBubble(C);
        }
        else if (HF_GameManager.hiddenScore == 45) {
            A.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("...or beef.");
            HandleThoughtBubble(A);
        }
        else if (HF_GameManager.hiddenScore == 60) {
            B.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("I could go for really any meat right now...");
            HandleThoughtBubble(B);
        }
        else if (HF_GameManager.hiddenScore == 75) {
            C.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("...meat.");
            HandleThoughtBubble(C);
        }
        else if (HF_GameManager.hiddenScore == 100) {
            thoughtTimer = 25f;
            HandleThoughtBubble(I_A);
        }
        else if (HF_GameManager.hiddenScore == 110) {
            HandleThoughtBubble(I_B);
        }
    }

    private IEnumerator WaitForThought(GameObject activeThought, float waitTime) {
        yield return new WaitForSeconds(waitTime);
        activeThought.SetActive(false);
    }

    private void HandleThoughtBubble(GameObject thought) {
        thought.SetActive(true);
        thoughtDisappear = WaitForThought(thought, thoughtTimer);
        StartCoroutine(thoughtDisappear);
    }
}
