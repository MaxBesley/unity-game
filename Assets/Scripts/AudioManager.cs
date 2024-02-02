using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Audio[] audios;
    public void Awake()
    {
        foreach (Audio audio in audios)
        {
            audio.audioSource = gameObject.AddComponent<AudioSource>();
            audio.audioSource.clip = audio.clip;
            audio.audioSource.volume = audio.volume;
            audio.audioSource.pitch = audio.pitch;
            audio.audioSource.loop = audio.loop;
        }
    }
    public void Play(string name)
    {
        Audio a = Array.Find(audios, audio => audio.name == name);
        if (a == null)
        {
            Debug.Log("Sound" + name + "notFound");
            return;
        }
        a.audioSource.Play();
    }
}
