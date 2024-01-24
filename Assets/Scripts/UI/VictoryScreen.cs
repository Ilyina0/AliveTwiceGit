using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private VoidEventChannel levelClearedEventChannel;
    [SerializeField] private StringEventChannel clearTimeTextEventChannel;
    [SerializeField] private Text timeText;

    private void OnEnable()
    {
        levelClearedEventChannel.AddListener(OnLevelCleared);
        clearTimeTextEventChannel.AddListener(SetTimeText);
    }

   

    private void OnDisable()
    {
        levelClearedEventChannel.RemoveListener(OnLevelCleared);
        clearTimeTextEventChannel.RemoveListener(SetTimeText);
    }

    private void OnLevelCleared()
    {
        ShowUI();
    }
    private void SetTimeText(string obj)
    {
        timeText.text = obj;
    }
    private void ShowUI()
    {
        GetComponent<Canvas>().enabled = true;
        GetComponent<Animator>().enabled = true;
    }
}
