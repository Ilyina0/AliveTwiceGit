using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 有限状态机的状态功能接口
/// </summary>
public interface IState
{
    void Enter();
    void Exit();
    void LogicUpdate();
    void PhysicUpdate();
}
