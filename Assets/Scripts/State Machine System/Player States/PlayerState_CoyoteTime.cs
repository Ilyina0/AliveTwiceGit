using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/CoyoteTime",fileName = "PlayerState_CoyoteTime")]
public class PlayerState_CoyoteTime : PlayerState
{
    [Tooltip("目标速度")]public float runSpeed = 5f;
    [SerializeField,Tooltip("土狼时间"),Range(0,0.2f)] private float CoyoteTime = 0.1f;
    public override void Enter()
    {
        base.Enter();
        //Set Gravity to 0
        _controller.SetUseGravity(false);
    }

    public override void LogicUpdate()
    {
        if (_input.Jump)
        {
            //土狼时间内可以跳跃
            _playerStateMachine.SwitchState(typeof(PlayerState_JumpUp));
        }

        if (stateDuration >= CoyoteTime || !_input.Move)
        {
            //土狼时间已到或者没有输入则转变成下落状态
            _playerStateMachine.SwitchState(typeof(PlayerState_Fall));
        }
    }

    public override void PhysicUpdate()
    {
        //Unity酱物理移动
        _controller.Move(runSpeed);
    }

    public override void Exit()
    {
        //Set Gravity to Normal
        _controller.SetUseGravity(true);
    }
}
