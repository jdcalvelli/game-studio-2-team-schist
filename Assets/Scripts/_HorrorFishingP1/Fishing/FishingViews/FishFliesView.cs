using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class FishFliesView : MonoBehaviour
{
    [SerializeField] private string[] flyTextOptions;
    [SerializeField] private TextMeshProUGUI[] flyTextBoxes;

    public void Animate_FliesSwarming() {
        for (int i = 0; i < flyTextBoxes.Length; i++) {
            //flyTextBoxes[i].transform.DOShakePosition(f, new Vector3(0.15f, 0.15f, 0f), 10, 0f, false, false);
        }
    }

    public void SetFlyTextOptions() {
        for (int i = 0; i < flyTextBoxes.Length; i++) {
            flyTextBoxes[i].text = flyTextOptions[Random.Range(0, flyTextOptions.Length)];
        }
    }
}
