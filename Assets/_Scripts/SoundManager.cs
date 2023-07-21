using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    [SerializeField] private float MusicVol = 0.8f;
    [SerializeField] private float SoundVol = 1f;

    public enum clipID {
        BUTTON,
        CAPYFOUND,
        COLLECTABLE,
        JUMP,
        DOOR,
        CABLES,
        MUSIC
    }
    
    [SerializeField] private AudioSource _audio;
    [SerializeField] private List<AudioClip> _clips;

    private void Start() {
        _audio.clip= AudioFromID(clipID.MUSIC);
        _audio.loop = true;
        _audio.volume = MusicVol;
        _audio.Play();
    }

    public void PlaySimple(clipID id) {
        _audio.PlayOneShot(AudioFromID(id), SoundVol);
    }

    public AudioClip AudioFromID(clipID id) {
        switch (id) {
            case clipID.BUTTON:
                return _clips[0];
            case clipID.CAPYFOUND:
                return _clips[1];
            case clipID.COLLECTABLE:
                return _clips[2];
            case clipID.JUMP:
                return _clips[3];
            case clipID.DOOR:
                return _clips[4];
            case clipID.CABLES:
                return _clips[5];
            case clipID.MUSIC:
                return _clips[6];
        }
        return _clips[0];
    }
}
