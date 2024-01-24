using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/JumpUp",fileName = "PlayerState_JumpUp")]
public class PlayerState_JumpUp : PlayerState
{
    [SerializeField,Tooltip("跳跃目标速度")] private float jumpHeight;
    [SerializeField,Tooltip("空中可移动速度")] private float moveSpeed;
    [SerializeField, Tooltip("跳跃粒子特效")] private ParticleSystem JumpVFX;
    [SerializeField, Tooltip("跳跃语音")] private AudioClip jumpVoice;
    private float jumpUpSpeed => Mathf.Sqrt(-2 * jumpHeight * Physics.gravity.y );
    public override void Enter()
    {
        base.Enter();
        //播放语音
        _controller.VoicePlayer.PlayOneShot(jumpVoice);
        //对刚体施加跳跃力
        _controller.SetVelocityY(jumpUpSpeed);
        Instantiate(JumpVFX,_controller.transform.position,Quaternion.identity);
    }

    public override void LogicUpdate()
    {
        if (_input.StopJump || _controller.isFalling)
        {
            //切换到下落状态
            _playerStateMachine.SwitchState(typeof(PlayerState_Fall));
        }
    }

    public override void PhysicUpdate()
    {
        _controller.Move(moveSpeed);
    }
}
