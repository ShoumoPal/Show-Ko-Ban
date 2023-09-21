using System;
using UnityEngine;

// public enum for sound type
public enum SoundType
{
    Button_Click,
    Move_Sound,
    Win_Sound,
    Connect_Sound,
    BG_Music_1,
    Wall_Hit_Sound,
    PopUp_Sound
}

// public class for sounds
[Serializable]
public class Sound
{
    public SoundType Type;
    [Range(0f, 1f)] public float Volume;
    public AudioClip Clip;
}

// AudioService which manages game audio
public class AudioService : GenericMonoSingleton<AudioService>
{
    [SerializeField] private Sound[] _sounds;
    [SerializeField] private AudioSource _soundFX;
    [SerializeField] private AudioSource _soundFX2;
    [SerializeField] private AudioSource _soundBG;

    public void PlayFX(SoundType type)
    {
        Sound sound = Array.Find(_sounds, i => i.Type == type);
        _soundFX.clip = sound.Clip;
        _soundFX.volume = sound.Volume;
        _soundFX.Play();
    }

    public void PlayFX2(SoundType type)
    {
        Sound sound = Array.Find(_sounds, i => i.Type == type);
        _soundFX2.clip = sound.Clip;
        _soundFX2.volume = sound.Volume;
        _soundFX2.Play();
    }

    public void PlayBG(SoundType type)
    {
        Sound sound = Array.Find(_sounds, i => i.Type == type);
        _soundBG.clip = sound.Clip;
        _soundBG.volume = sound.Volume;
        _soundBG.loop = true;
        _soundBG.PlayDelayed(0.5f);
    }
}

