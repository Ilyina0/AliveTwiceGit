using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneParameterEventChannel<T> : ScriptableObject
{
    public event Action<T> Delegate;

    /// <summary>
    /// 广播事件(调用）
    /// </summary>
    public void Broadcast(T obj)
    {
        Delegate?.Invoke(obj);
    }
    /// <summary>
    /// 给事件添加订阅者
    /// </summary>
    /// <param name="action">订阅者响应事件</param>
    public void AddListener(Action<T> action)
    {
        Delegate += action;
    }
    /// <summary>
    /// 移除订阅者
    /// </summary>
    /// <param name="action">订阅者响应事件</param>
    public void RemoveListener(Action<T> action)
    {
        Delegate -= action;
    }
}
