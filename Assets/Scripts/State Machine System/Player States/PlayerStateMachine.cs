using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    #region 状态资源

    [SerializeField] private List<PlayerState> states;

    #endregion
    
    
    private Animator _animator;
    private PlayerInput _input;
    private PlayerController _playerController;
    
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _input = GetComponent<PlayerInput>();
        _playerController = GetComponent<PlayerController>();
        stateTable = new Dictionary<Type, IState>(states.Count);
        //playerStates initialization here
        foreach (var state in states)
        {
            state.Initialize(_animator, _input, _playerController,this);
            stateTable.Add(state.GetType(),state);
        }
    }
    
    private void Start()
    {
        SwitchOn(typeof(PlayerState_Idle));
    }

    public void UpdateFlesh(Animator animator)
    {
        foreach (var state in states)
        {
            state.ChangeFlesh(animator);
        }
    }
}
