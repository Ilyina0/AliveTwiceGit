using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/SoulMove",fileName = "PlayerState_SoulOperation")]
public class PlayerState_SoulOperation : PlayerState
{
    [SerializeField] private TransformEventChannel soulSeparationEventChannel;
    [SerializeField] private float soulMoveSpeed;
    
    public override void Enter()
    {
        base.Enter();
        //TODO 灵魂体的动画播放
        //事件广播
        soulSeparationEventChannel.Broadcast(_controller.Soul.transform);
    }

    public override void LogicUpdate()
    {
        if (_input.Move_Soul)
        {
            Vector2 moveDirection = new Vector2(_input.AxisX_Soul,_input.AxisY_Soul ).normalized;
            Vector2 moveDistance = moveDirection * (soulMoveSpeed * Time.deltaTime);
            _controller.SoulMove(moveDistance);
        }
        
        if (_input.ChangeToFlesh)
        {
            //回到肉体操作装态
            _playerStateMachine.SwitchState((typeof(PlayerState_Idle)));
        }

        if (_input.SpiritPossession)
        {
            if (_controller.Soul.ISHavePossessableFlesh)
            {
                _playerStateMachine.SwitchState(typeof(PlayerState_Possessed));
            }
        }
    }
}
