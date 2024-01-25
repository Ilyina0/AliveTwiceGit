using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Idle",fileName = "PlayerState_Idle")]
public class PlayerState_Idle : PlayerState
{
   public float deceleration = 20f;
   [SerializeField] private TransformEventChannel changeToFlashEventChannel;

   public override void Enter()
   {
      base.Enter();
      //禁用灵魂操作，开启肉体操作
      _input.DisableSoulPlayInputs();
      _input.EnableGamePlayInputs();
      //事件广播
      changeToFlashEventChannel.Broadcast(_controller.transform);
      
      currentSpeed = _controller.moveSpeed;
   }

   public override void LogicUpdate()
   {
      _controller.OnWayPlatformCheck();
      if (_input.FleshPossession && _controller.isSoulSeparation)
      {
         if (_controller.Flesh.ISHavePossessableSoul)
         {
            _playerStateMachine.SwitchState(typeof(PlayerState_FleshPossessed));
         }
      }
      if (_input.Move)
      {
         //切换到跑步状态
         _playerStateMachine.SwitchState(typeof(PlayerState_Run));
      }

      if (_input.Jump)
      {
         //切换到跳跃状态
         _playerStateMachine.SwitchState(typeof(PlayerState_JumpUp));
      }
      
      if (!_controller.isGround)
      {
         //切换到下落状态
         _playerStateMachine.SwitchState(typeof(PlayerState_Fall));
      }
      
      if (_input.SoulSeparation)
      {
         //切换到灵魂出鞘状态
         _playerStateMachine.SwitchState(typeof(PlayerState_SoulSeparation));
      }
      currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
   }

   public override void PhysicUpdate()
   {
      //玩家减速过程
      _controller.SetVelocityX(currentSpeed * _controller.transform.localScale.x);
   }
}
