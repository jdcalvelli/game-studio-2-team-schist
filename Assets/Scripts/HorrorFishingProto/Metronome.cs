using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Metronome : MonoBehaviour
{
    private float metronomeTimer;
    private float graceTimer;

    private float hitDisplayTimer;
    private bool metronomePassed;
    
    [SerializeField] private float metronomeMaxTime = 3f;
    [SerializeField] private float graceMaxTime = 0.3f;
    [SerializeField] private SpriteRenderer spriteDisplay;

    [SerializeField] private TextMeshProUGUI hitDisplay;
    [SerializeField] private TextMeshProUGUI timerDisplay;


    private void Start() {
        metronomeTimer = metronomeMaxTime;
        graceTimer = graceMaxTime;

        spriteDisplay.color = Color.white;

        metronomePassed = false;
        hitDisplayTimer = 0.5f;
    }

    private void Update() {
        timerDisplay.SetText((Mathf.Round(metronomeTimer * 10f) / 10f).ToString());

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
        }
        else if (metronomeTimer <= 0f && graceTimer > 0f) {
            metronomeTimer = 0f;
            spriteDisplay.color = Color.green;
            graceTimer -= Time.deltaTime;
            hitDisplay.SetText("(!!!)");
            if (Input.GetKeyDown("space")) {
                hitDisplay.SetText("nice");
                ResetTimers();
            }
        }
        else if (metronomeTimer <= 0f && graceTimer <= 0f) {
            hitDisplay.SetText("miss");
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
