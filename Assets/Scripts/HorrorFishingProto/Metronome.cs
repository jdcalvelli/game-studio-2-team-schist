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

    private Color lerpColor;


    private void Start() {
        metronomeTimer = metronomeMaxTime;
        graceTimer = graceMaxTime;

        lerpColor = Color.red;
        spriteDisplay.color = lerpColor;

        metronomePassed = false;
        hitDisplayTimer = 0.5f;
        hitDisplay.gameObject.SetActive(true);
    }

    private void Update() {
        timerDisplay.SetText(metronomeTimer.ToString());

        if (metronomePassed) {
            if (hitDisplayTimer > 0f) {
                hitDisplayTimer -= Time.deltaTime;
                hitDisplay.gameObject.SetActive(true);
            }
            else {
                hitDisplayTimer = 0.5f;
                hitDisplay.gameObject.SetActive(false);
                metronomePassed = false;
            }
        }
        
        if (metronomeTimer > 0f) {
            metronomeTimer -= Time.deltaTime;
        }
        else if (metronomeTimer <= 0f && graceTimer > 0f) {
            metronomeTimer = 0f;
            spriteDisplay.color = Color.blue;
            graceTimer -= Time.deltaTime;
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
        spriteDisplay.color = Color.red;
    }
}
