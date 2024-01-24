using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Run",fileName = "PlayerState_Run")]
public class PlayerState_Run : PlayerState
{
    [Tooltip("目标速度")]public float runSpeed = 5f;
    [Tooltip("加速度")]public float acceleration = 5f;
    public override void Enter()
    {
        base.Enter();
        currentSpeed = _controller.moveSpeed;
    }

    public override void LogicUpdate()
    {
        if (!_input.Move)
        {
            //切换到空闲状态
            _playerStateMachine.SwitchState(typeof(PlayerState_Idle));
        }
        if (_input.Jump)
        {
            //切换到跳跃状态
            _playerStateMachine.SwitchState(typeof(PlayerState_JumpUp));
        }

        if (!_controller.isGround)
        {
            //切换到土狼时间状态
            _playerStateMachine.SwitchState(typeof(PlayerState_CoyoteTime));
        }
        currentSpeed = Mathf.MoveTowards(currentSpeed, runSpeed, acceleration * Time.deltaTime);
    }

    public override void PhysicUpdate()
    {
        //Unity酱物理移动
        _controller.Move(currentSpeed);
    }
}
