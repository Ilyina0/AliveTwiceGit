using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DefeatScreen : MonoBehaviour
{
    [SerializeField] private VoidEventChannel PlayerDeadEventChannel;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button quitButton;
    private void OnEnable()
    {
        PlayerDeadEventChannel.AddListener(OnPlayerDead);
        retryButton.onClick.AddListener(SceneLoader.ReloadScene);
        quitButton.onClick.AddListener(SceneLoader.QuitGame);
    }

    private void OnDisable()
    {
        PlayerDeadEventChannel.RemoveListener(OnPlayerDead);
        retryButton.onClick.RemoveListener(SceneLoader.ReloadScene);
        quitButton.onClick.RemoveListener(SceneLoader.QuitGame);
    }

    private void OnPlayerDead()
    {
        ShowUI();
    }

    private void ShowUI()
    {
        GetComponent<Canvas>().enabled = true;
        GetComponent<Animator>().enabled = true;
    }
}
