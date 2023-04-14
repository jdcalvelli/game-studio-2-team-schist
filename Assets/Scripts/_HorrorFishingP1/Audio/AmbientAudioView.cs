using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientAudioView : MonoBehaviour
{
    [SerializeField] private AudioSource ambientAudioSource;
    [SerializeField] private AudioClip[] ambientBGMs;

    public void PlayAmbientAudio() {
        if (ambientAudioSource.isPlaying == false) {
            ambientAudioSource.PlayOneShot(ambientBGMs[Random.Range(0, ambientBGMs.Length)]);
        }
    }
}
