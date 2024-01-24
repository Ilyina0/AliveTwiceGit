using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : Singleton<BGMPlayer>
{
    private AudioSource _audioSource;

    protected override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = true;
        _audioSource.loop = true;
    }
}
