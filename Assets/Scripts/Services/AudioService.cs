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
    PopUp_Sound,
    Interference,
    Interstellar
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
    [SerializeField] private AudioSource _soundFX3;
    [SerializeField] private AudioSource _soundBG;
    [SerializeField] private AudioSource _soundBG2;

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

    public void PlayFX3(SoundType type)
    {
        Sound sound = Array.Find(_sounds, i => i.Type == type);
        _soundFX3.clip = sound.Clip;
        _soundFX3.volume = sound.Volume;
        _soundFX3.Play();
    }

    public void PlayBG(SoundType type)
    {
        Sound sound = Array.Find(_sounds, i => i.Type == type);
        _soundBG.clip = sound.Clip;
        _soundBG.volume = sound.Volume;
        _soundBG.loop = true;
        _soundBG.PlayDelayed(0.5f);
    }

    public void PlayBG2(SoundType type)
    {
        Sound sound = Array.Find(_sounds, i => i.Type == type);
        _soundBG2.clip = sound.Clip;
        _soundBG2.volume = sound.Volume;
        _soundBG2.loop = true;
        _soundBG2.PlayDelayed(0.5f);
    }

    public void StopBG2()
    {
        _soundBG2.Stop();
    }
}

