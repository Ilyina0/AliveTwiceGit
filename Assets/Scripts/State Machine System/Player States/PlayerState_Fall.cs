using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Fall",fileName = "PlayerState_Fall")]
public class PlayerState_Fall : PlayerState
{
   [SerializeField] private AnimationCurve speedCurve;
   [SerializeField,Tooltip("空中可移动速度")] private float moveSpeed;
   public override void LogicUpdate()
   {
      if (_controller.isGround)
      {
         _playerStateMachine.SwitchState(typeof(PlayerState_Land));
      }

      // if (_input.Jump && _controller.CanAirJump)
      // {
      //    //Switch to air jump state
      //    _playerStateMachine.SwitchState(typeof(PlayerState_AirJump));
      // }
      else if(_input.Jump && !_controller.CanAirJump)
      {
         //存储预输入
         _input.SetJumpInputBuffer();
      }
   }

   public override void PhysicUpdate()
   {
      _controller.Move(moveSpeed);
      _controller.SetVelocityY(speedCurve.Evaluate(stateDuration));
   }
}
