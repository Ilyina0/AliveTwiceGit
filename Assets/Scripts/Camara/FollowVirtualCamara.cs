using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FollowVirtualCamara : MonoBehaviour
{
    [SerializeField] private TransformEventChannel soulSeparateEventChannel;
    [SerializeField] private TransformEventChannel changeToFlashEventChannel;
    private CinemachineVirtualCamera _virtualCamera;

    private void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void OnEnable()
    {
        soulSeparateEventChannel.AddListener(OnSoulSeparate);
        changeToFlashEventChannel.AddListener(OnChangeToFlash);
    }

    private void OnDisable()
    {
        soulSeparateEventChannel.RemoveListener(OnSoulSeparate);
        changeToFlashEventChannel.RemoveListener(OnChangeToFlash);
    }

    private void OnSoulSeparate(Transform obj)
    {
        _virtualCamera.Follow = obj;
    }

    private void OnChangeToFlash(Transform obj)
    {
        _virtualCamera.Follow = obj;
    }
}
