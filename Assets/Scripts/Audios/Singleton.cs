using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T:Singleton<T>
{
    public static T Instance { get; private set; }
    /// <summary>
    /// 该类的单例是否初始化了，即是否存在实例
    /// </summary>
    public static bool IsInitialized => Instance != null;
    protected virtual void Awake()
    {
        if (Instance != null && Instance != (T)this)
        {
            Destroy(gameObject);
        }
        else if(Instance == null)
        {
            Instance = (T)this;
        }
    }

    protected virtual void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
