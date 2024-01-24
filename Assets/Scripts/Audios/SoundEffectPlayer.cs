using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : Singleton<SoundEffectPlayer>
{
    private AudioSource _audioSource;

    protected override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
    }

    public void playerOneShot(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}
