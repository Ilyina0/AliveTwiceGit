using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyScreen : MonoBehaviour
{
    [SerializeField] private AudioClip startVocice;
    [SerializeField] private VoidEventChannel levelStartEventChannel;
    //Animation End Event Function
    public void LevelStart()
    {
        levelStartEventChannel.Broadcast();
        GetComponent<Canvas>().enabled = false;
        GetComponent<Animator>().enabled = false;
    }

    void playStartVoice()
    {
        SoundEffectPlayer.Instance.playerOneShot(startVocice);
    }
}
