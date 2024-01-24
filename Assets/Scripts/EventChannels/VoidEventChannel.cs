using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/EventChannels/VoidEventChannel",fileName = "voidEventChannel")]
public class VoidEventChannel : ScriptableObject
{
    public event Action Delegate;

    /// <summary>
    /// 广播事件(调用）
    /// </summary>
    public void Broadcast()
    {
        Delegate?.Invoke();
    }
    /// <summary>
    /// 给事件添加订阅者
    /// </summary>
    /// <param name="action">订阅者响应事件</param>
    public void AddListener(Action action)
    {
        Delegate += action;
    }
    /// <summary>
    /// 移除订阅者
    /// </summary>
    /// <param name="action">订阅者响应事件</param>
    public void RemoveListener(Action action)
    {
        Delegate -= action;
    }
}
