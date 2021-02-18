using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class SoundArray {

    public string name;

    public AudioClip soundClip;

    [Range(0f, 1f)]
    public float soundVolume;

    [Range(.1f, 3f)]
    public float soundPitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
