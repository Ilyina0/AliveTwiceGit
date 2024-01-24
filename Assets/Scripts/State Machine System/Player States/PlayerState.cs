using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : ScriptableObject ,IState
{
    [SerializeField] protected string stateName;
    [SerializeField,Range(0f,1f),Tooltip("交叉淡化融合程度")] private float transitionDuration = 0.1f;
 
    private int AnimStateHash;
    private float stateStartTime;
    
    protected float currentSpeed;
    protected Animator _animator;
    protected PlayerInput _input;
    protected PlayerController _controller;
    protected PlayerStateMachine _playerStateMachine;
    /// <summary>
    /// 判断当前状态动画是否播放完毕
    /// </summary>
    protected bool CurrentAnimFinished => stateDuration >= _animator.GetCurrentAnimatorStateInfo(0).length;
    /// <summary>
    /// 当前状态动画播放的时间间隔
    /// </summary>
    protected float stateDuration => Time.time - stateStartTime;

    private void OnEnable()
    {
        AnimStateHash = Animator.StringToHash(stateName);
    }

    public void Initialize(Animator animator, PlayerInput input,PlayerController playerController,PlayerStateMachine playerStateMachine)
    {
        _animator = animator;
        _input = input;
        _controller = playerController;
        _playerStateMachine = playerStateMachine;
    }

    public void ChangeFlesh(Animator animator)
    {
        _animator = animator;
    }
    public virtual void Enter()
    {
        if (stateName == string.Empty)
        {
            _animator.CrossFade("Idle",stateStartTime);
        }
        else
        {
            _animator.CrossFade(AnimStateHash,transitionDuration);
        }
        stateStartTime = Time.time;
    }

    public virtual void Exit()
    {
        
    }

    public virtual void LogicUpdate()
    {
        
    }

    public virtual void PhysicUpdate()
    {
        
    }
}
