using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    #region 字段

    protected IState currentState;
    protected Dictionary<System.Type, IState> stateTable;

    #endregion

    #region Unity生命周期函数
    
    private void Update()
    {
        currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        currentState.PhysicUpdate();
    }

    #endregion

    #region 方法
    /// <summary>
    /// 启功状态机
    /// </summary>
    /// <param name="newState">状态机启动时的新状态</param>
    protected void SwitchOn(IState newState)
    {
        if(newState != null)
             currentState = newState;
        currentState.Enter();
    }
    /// <summary>
    /// 启功状态机
    /// </summary>
    /// <param name="newStateType">状态机启动时的新状态的类型</param>
    protected void SwitchOn(Type newStateType)
    {
        SwitchOn(stateTable[newStateType]);
    }
    /// <summary>
    /// 状态机状态切换
    /// </summary>
    /// <param name="newState">切换的新状态</param>
    public void SwitchState(IState newState)
    {
        if (newState == null) return;
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
    /// <summary>
    /// 状态机状态切换
    /// </summary>
    /// <param name="newStateType">切换的新状态的类型</param>
    public void SwitchState(Type newStateType)
    {
        SwitchState(stateTable[newStateType]);
    }
    #endregion
}