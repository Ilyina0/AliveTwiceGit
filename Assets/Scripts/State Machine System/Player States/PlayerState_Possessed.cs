using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Possessed",fileName = "PlayerState_Possessed")]
public class PlayerState_Possessed : PlayerState
{
    private Transform possessedFlesh;
    private float minDistance = 1e8f;
    [SerializeField] private float possessedMoveSpeed;
    [SerializeField] private VoidEventChannel soulPossessEventChannel;
    public override void Enter()
    {
        base.Enter();
        _input.DisableSoulPlayInputs();
        minDistance = 1e8f;
        foreach (var item in _controller.Soul.nearbyFlesh)
        {
            if(item == null) break;
            if (Vector3.Distance(_controller.Soul.transform.position, item.transform.position) < minDistance)
            {
                possessedFlesh = item.transform;
                minDistance = Vector3.Distance(_controller.Soul.transform.position, item.transform.position);
            }
        }
        if (possessedFlesh.CompareTag("Player"))
        {
            //停止肉体消融
            possessedFlesh.GetComponent<Flesh>()?.StopExtinguishing();
        }
        else if (possessedFlesh.CompareTag("Enemy") || possessedFlesh.CompareTag("Ownerless"))
        {
            //将原来肉体消融
            _controller.Flesh.Extinguishing();
            //TODO 消耗一格能量
        }
        
    }
    public override void LogicUpdate()
    {
        if(_controller.Soul == null) return;
        if (Vector3.Distance(_controller.Soul.transform.position, possessedFlesh.position) > float.Epsilon)
        {
            _controller.Soul.transform.position = Vector3.MoveTowards(_controller.Soul.transform.position,
                possessedFlesh.position, possessedMoveSpeed * Time.deltaTime);
        }
        else
        {
            _playerStateMachine.SwitchState(typeof(PlayerState_Idle));
        }
    }

    public override void Exit()
    {
        soulPossessEventChannel.Broadcast();
        if (possessedFlesh.CompareTag("Enemy")|| possessedFlesh.CompareTag("Ownerless"))
        {
            //将现在肉体移到Player下
            _controller.transform.position = possessedFlesh.position;
            //销毁敌人原来敌人肉体
            Destroy(possessedFlesh.gameObject);
            //生成玩家的新肉体
            var newFlesh = Instantiate(_controller.playerFleshPrefab, _controller.transform);
            newFlesh.transform.localPosition = Vector3.zero;
            _controller.Flesh = newFlesh.GetComponent<Flesh>();
            //更新状态机动画控制器
            _controller.GetComponent<PlayerStateMachine>().UpdateFlesh(newFlesh.GetComponent<Animator>());
        }
    }
}
