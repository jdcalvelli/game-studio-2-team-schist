using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientAudioManager : MonoBehaviour
{
    [SerializeField] private AmbientAudioView _ambientAudioView;

    public void StartRandomBGM() {
        _ambientAudioView.PlayAmbientAudio();
    }
}
