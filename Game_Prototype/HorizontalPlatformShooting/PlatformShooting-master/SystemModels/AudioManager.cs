using System;
using UnityEngine;
using Random = UnityEngine.Random;

//持久单例，音频管理器，用于音频的播放
public class AudioManager : PersistentSingleton<AudioManager>
{
    AudioSource sfxPlayer;
    
    void Start()
    {
        sfxPlayer = GetComponent<AudioSource>();
    }

    //播放音频
    public void PlaySFX(AudioData audio)
    {
        sfxPlayer.PlayOneShot(audio.audioClip,audio.volume);
    }
    
    public void PlaySFX(AudioData[] audio)
    {
        int ranint = Random.Range(0, audio.Length);
        sfxPlayer.PlayOneShot(audio[ranint].audioClip,audio[ranint].volume);
    }
    
}

[Serializable]public class AudioData
{
    public AudioClip audioClip;
    public float volume;
}