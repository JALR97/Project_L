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
                break;
            case clipID.CAPYFOUND:
                return _clips[1];
                break;
            case clipID.COLLECTABLE:
                return _clips[2];
                break;
            case clipID.JUMP:
                return _clips[3];
                break;
            case clipID.DOOR:
                return _clips[4];
                break;
            case clipID.CABLES:
                return _clips[5];
                break;
            case clipID.MUSIC:
                return _clips[6];
                break;
        }

        return _clips[0];
    }
}
