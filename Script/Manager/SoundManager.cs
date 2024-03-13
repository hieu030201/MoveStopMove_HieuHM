using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SoundType
{
    SoundClick = 0,
    SoundAttack = 1,
    SoundOnHit = 2,
}
public class SoundManager : Singleton<SoundManager>
{
    public Transform player;
    public float maxDistance = 10f;
    public float minVolume = 0.1f;
    public float maxVolume = 1f;
    private bool offSound = false;
    public List<SoundItem> soundAudioSources = new List<SoundItem>();
    public void PlayMusic()
    {
        offSound = false;
        for (int i = 0; i < soundAudioSources.Count; i++)
        {
            soundAudioSources[i].audioSource.volume = 1f;
        }
    }
    public void StopMusic()
    {
        offSound = true;
        for (int i = 0; i < soundAudioSources.Count; i++)
        {
            soundAudioSources[i].audioSource.volume = 0f;
        }
    }
    public void PlayOnShot(SoundType type, float volumeValue)
    {
        if (!offSound)
        {
            SoundItem soundItem = soundAudioSources.Find(item => item.type == type);
            if (soundItem != null)
            {
                soundItem.audioSource.PlayOneShot(soundItem.audioSource.clip);
                soundItem.audioSource.volume = volumeValue;
            }
        }
    }
    public void PlayOnShot(SoundType type)
    {
        if (!offSound)
        {
            SoundItem soundItem = soundAudioSources.Find(item => item.type == type);
            if (soundItem != null)
            {
                soundItem.audioSource.PlayOneShot(soundItem.audioSource.clip);
            }
        }

    }
    public void PlayMusic(SoundType type)
    {
        if (offSound)
        {
            SoundItem soundItem = soundAudioSources.Find(item => item.type == type);

            if (soundItem != null)
            {
                soundItem.audioSource.Play();
            }
        }

    }

}
[System.Serializable]

public class SoundItem
{
    public SoundType type;
    public AudioSource audioSource;
}

