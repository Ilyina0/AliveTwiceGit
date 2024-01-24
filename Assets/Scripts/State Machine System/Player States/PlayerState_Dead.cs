
using UnityEngine;


[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Dead",fileName = "PlayerState_Dead")]
public class PlayerState_Dead : PlayerState
{
    [SerializeField] private ParticleSystem DeadVFX;
    [SerializeField] private AudioClip[] DeadVoice;
    [SerializeField] private VoidEventChannel playerDeadEventChannel;

    public override void Enter()
    {
        base.Enter();
        Instantiate(DeadVFX, _controller.transform.position, Quaternion.identity);
        var voice = DeadVoice[Random.Range(0, DeadVoice.Length)];
        _controller.VoicePlayer.PlayOneShot(voice);
    }

    public override void LogicUpdate()
    {
        //死亡动画播完后进入漂浮状态
        if (CurrentAnimFinished)
        {
            playerDeadEventChannel.Broadcast();
        }
    }
    
}
