using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/FleshPossessed",fileName = "PlayerState_FleshPossessed")]
public class PlayerState_FleshPossessed : PlayerState
{
    private Transform possessedSoul;
    private float minDistance = 1e8f;
    [SerializeField] private float possessedMoveSpeed;
    [SerializeField] private VoidEventChannel FleshPossessEventChannel;
    public override void Enter()
    {
        base.Enter();
        _input.DisableGamePlayInputs();
        minDistance = 1e8f;
        foreach (var item in _controller.Flesh.NearbySouls)
        {
            if(item == null) break;
            if (Vector3.Distance(_controller.transform.position, item.transform.position) < minDistance)
            {
                possessedSoul = item.transform;
                minDistance = Vector3.Distance(_controller.transform.position, item.transform.position);
            }
        }
        if (possessedSoul.CompareTag("Player"))
        {
            //停止灵魂消融
            possessedSoul.GetComponent<Soul>().StopExtinguishing();
        }
        else if(possessedSoul.CompareTag("Enemy")|| possessedSoul.CompareTag("Ownerless"))
        {
            //将原来的灵魂消融
            _controller.Soul.Extinguishing();
            //TODO 消耗一格能量
        }
        
    }
    public override void LogicUpdate()
    {
        if(_controller.Soul == null) return;
        if (Vector3.Distance(_controller.transform.position, possessedSoul.position) > float.Epsilon)
        {
            possessedSoul.position = Vector2.MoveTowards(possessedSoul.position, _controller.transform.position,
                possessedMoveSpeed * Time.deltaTime);
        }
        else
        {
            _playerStateMachine.SwitchState(typeof(PlayerState_Idle));
        }
    }

    public override void Exit()
    {
        FleshPossessEventChannel.Broadcast();
        //销毁附身灵魂
        Destroy(possessedSoul.gameObject);
    }
}
