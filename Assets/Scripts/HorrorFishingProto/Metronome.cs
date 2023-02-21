using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Metronome : MonoBehaviour
{
    private float metronomeTimer;
    private float graceTimer;
    private float metronomeMaxTime = 2f;
    private float graceMaxTime = 0.25f;

    [SerializeField] private Image spriteDisplay;
    [SerializeField] private TextMeshProUGUI hitDisplay;
    [SerializeField] private TextMeshProUGUI timerDisplay;

    private float hitDisplayTimer;
    private bool metronomePassed;

    private int combo;


    private void Start() {
        combo = 0;
        metronomeTimer = metronomeMaxTime;
        graceTimer = graceMaxTime;

        spriteDisplay.color = Color.white;

        metronomePassed = false;
        hitDisplayTimer = 0.5f;
    }

    private void Update() {
        //timerDisplay.SetText((Mathf.Round(metronomeTimer * 10f) / 10f).ToString());
        if (HF_GameManager.hiddenScore <= 110) {
            timerDisplay.SetText("(x" + combo.ToString() + ")");
        }
        else {
            timerDisplay.SetText("ughh");
        }

        if (HF_GameManager.hiddenScore >= 15 && HF_GameManager.hiddenScore <= 24) {
            metronomeMaxTime = 1.5f;
        }
        else if (HF_GameManager.hiddenScore >= 25 && HF_GameManager.hiddenScore <= 35) {
            metronomeMaxTime = 1.1f;
        }
        else if (HF_GameManager.hiddenScore >= 40) {
            metronomeMaxTime = 0.75f;
        }

        if (metronomePassed) {
            if (hitDisplayTimer > 0f) {
                hitDisplayTimer -= Time.deltaTime;
            }
            else {
                hitDisplayTimer = 0.5f;
                hitDisplay.SetText("");
                metronomePassed = false;
            }
        }
        
        if (metronomeTimer > 0f) {
            metronomeTimer -= Time.deltaTime;
            spriteDisplay.color = Color.Lerp(Color.green, Color.white, metronomeTimer);
            if (Input.GetKeyDown("space")) {
                hitDisplay.SetText("miss");
                combo =0;
                ResetTimers();
            }
        }
        else if (metronomeTimer <= 0f && graceTimer > 0f) {
            metronomeTimer = 0f;
            spriteDisplay.color = Color.green;
            graceTimer -= Time.deltaTime;
            hitDisplay.SetText("(!!!)");
            if (Input.GetKeyDown("space")) {
                hitDisplay.SetText("nice");
                HF_GameManager.hiddenScore++;
                combo++;
                ResetTimers();
            }
        }
        else if (metronomeTimer <= 0f && graceTimer <= 0f) {
            hitDisplay.SetText("miss");
            combo = 0;
            ResetTimers();
        }
    }

    private void ResetTimers() {
        metronomePassed = true;
        metronomeTimer = metronomeMaxTime;
        graceTimer = graceMaxTime;
        spriteDisplay.color = Color.white;
    }
}
