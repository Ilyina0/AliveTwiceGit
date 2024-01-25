using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Land",fileName = "PlayerState_Land")]
public class PlayerState_Land : PlayerState
{
    [SerializeField,Tooltip("落地硬直时间"),Range(0,0.5f)] private float stiffTime = 0.2f;
    [SerializeField] private ParticleSystem landVFX;
    public override void Enter()
    {
        base.Enter();
        
        //落地归零速度
        _controller.SetVelocity(Vector3.zero);
        //特效
        Instantiate(landVFX, _controller.transform.position, Quaternion.identity);
    }

    public override void LogicUpdate()
    {
        // if (_controller.Victory)
        // {
        //     _playerStateMachine.SwitchState(typeof(PlayerState_Victory));
        // }
        if (_input.Jump || _input.HasJumpInputBuffer)
        {
            _playerStateMachine.SwitchState(typeof(PlayerState_JumpUp));
        }
        _controller.OnWayPlatformCheck();
        //落地硬直
        if(stateDuration < stiffTime) return;
        if (_input.Move)
        {
            _playerStateMachine.SwitchState(typeof(PlayerState_Run));
        }
        
        if (CurrentAnimFinished)
        {
            _playerStateMachine.SwitchState(typeof(PlayerState_Idle));
        }
    }

    public override void Exit()
    {
        //落地用了预输入，清除
        _input.HasJumpInputBuffer = false;
    }
}
