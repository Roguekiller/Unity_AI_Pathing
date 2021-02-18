using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{

    public SoundArray[] sounds;

    // Use this for initialization
    void Awake()
    {
        foreach (SoundArray s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.soundClip;

            s.source.volume = s.soundVolume;
            s.source.pitch = s.soundPitch;
            s.source.loop = s.loop;
        }
    }

    public void Play (string name)
    {
        SoundArray s = Array.Find(sounds, sound => sound.name == name);
        if (s == null){
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
}

