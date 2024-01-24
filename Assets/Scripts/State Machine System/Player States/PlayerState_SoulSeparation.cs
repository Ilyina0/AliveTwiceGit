using UnityEngine;
using UnityEngine.Animations;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/SoulSeparation",fileName = "PlayerState_SoulSeparation")]
public class PlayerState_SoulSeparation : PlayerState
{
    
    public override void Enter()
    {
        base.Enter();
        //变换输入方案
        _input.DisableGamePlayInputs();
        _input.EnableSoulPlayInputs();
        if (!_controller.isSoulSeparation)
        {
            //生成灵魂体
            var Soul = Instantiate(_controller.soulPrefab);
            _controller.Soul = Soul.GetComponent<Soul>();
            var transform = _controller.Soul.transform;
            var transform1 = _controller.transform;
            transform.position = (Vector3)_controller.soulGenerationOffset + transform1.position;
            transform.rotation = Quaternion.identity;
            transform.localScale = transform1.localScale;
            //TODO 灵魂体的分离动画播放
        }
        else
        {
            //回到灵魂操作即可
            _playerStateMachine.SwitchState(typeof(PlayerState_SoulOperation));
        }
        
        //肉体速度归零
        _controller.SetVelocity(Vector3.zero);
        
    }

    public override void LogicUpdate()
    {
        //TODO 灵魂分离动画播完后正式进入灵魂操控状态
        // if (CurrentAnimFinished)
        // { 
        //     _playerStateMachine.SwitchState(typeof(PlayerState_SoulOperation));
        // }
        //模拟测试
        if (stateDuration > 0.5f)
        {
            _playerStateMachine.SwitchState(typeof(PlayerState_SoulOperation));
        }
    }
}
