using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearTimer : MonoBehaviour
{
    [SerializeField] private Text timeText;
    [SerializeField] private VoidEventChannel levelStartEventChannel;
    [SerializeField] private VoidEventChannel levelClearEventChannel;
    [SerializeField] private StringEventChannel cleartimeTextEventChannel;
    [SerializeField] private VoidEventChannel playerDeadEventChannel;
    private bool stop = true;
    private float clearTime;

    private void OnEnable()
    {
        levelStartEventChannel.AddListener(LevelStart);
        levelClearEventChannel.AddListener(LevelClear);
        playerDeadEventChannel.AddListener(HideUI);
    }

    private void OnDisable()
    {
        levelStartEventChannel.RemoveListener(LevelStart);
        levelClearEventChannel.RemoveListener(LevelClear);
        playerDeadEventChannel.RemoveListener(HideUI);
    }

    private void LevelClear()
    {
        HideUI();
        cleartimeTextEventChannel.Broadcast(timeText.text);
    }

    private void LevelStart()
    {
        stop = false;
    }

    private void FixedUpdate()
    {
        if(stop) return;
        clearTime += Time.fixedDeltaTime;
        timeText.text = TimeSpan.FromSeconds(clearTime).ToString(@"mm\:ss\:ff");
    }

    private void HideUI()
    {
        stop = true;
        GetComponent<Canvas>().enabled = false;
    }
}
