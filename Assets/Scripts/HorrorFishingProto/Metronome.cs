using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    private float metronomeTimer;
    private float graceTimer;
    
    [SerializeField] private float metronomeMaxTime = 3f;
    [SerializeField] private float graceMaxTime = 0.3f;
    [SerializeField] private SpriteRenderer spriteDisplay;

    private void Start() {
        metronomeTimer = metronomeMaxTime;
        graceTimer = graceMaxTime;
    }

    private void Update() {
        
        if (metronomeTimer > 0f) {
            metronomeTimer -= Time.deltaTime;
        }
        else if (metronomeTimer <= 0f && graceTimer > 0f) {
            metronomeTimer = 0f;
            spriteDisplay.color = Color.blue;
            graceTimer -= Time.deltaTime;
            if (Input.GetKeyDown("space")) {
                
                ResetTimers();
            }
        }
        else if (metronomeTimer <= 0f && graceTimer <= 0f) {
            ResetTimers();
        }
    }

    private void ResetTimers() {
        metronomeTimer = metronomeMaxTime;
        graceTimer = graceMaxTime;
        spriteDisplay.color = Color.red;
    }
}
