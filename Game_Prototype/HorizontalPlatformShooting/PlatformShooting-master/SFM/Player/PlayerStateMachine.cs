using System;
using System.Collections.Generic;
using Input;
using SFM.Player.PlayerStates;
using State_Machine_System.Base;
using UnityEngine;

namespace SFM.Player
{
    public class PlayerStateMachine : StateMachine
    {
        //public PlayerState_Idle idleState;
        //public PlayerState_Run runState;
        //一个个地初始化显然不符合

        [SerializeField] PlayerState[] states;
        Animator _animator;
        PlayerInput _playerInput; 
        PlayerController _playerController;
        CapsuleCollider2D _collider2D;
        void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _playerInput = GetComponent<PlayerInput>();
            _playerController = GetComponent<PlayerController>();
            _collider2D = GetComponent<CapsuleCollider2D>();
            
            //Do player states initialization here
            //idleState.Initialize(_animator,this);
            //runState.Initialize(_animator,this);
            stateTable = new Dictionary<Type, IState>(states.Length);
            foreach (var state in states)
            {
                state.Initialize(_animator,_playerInput,_playerController,this, _collider2D);
                stateTable.Add(state.GetType(),state);
            }
        }
        void Start()
        {
            SwitchOn(stateTable[typeof(PlayerState_Idle)]);//根据类型从字典中去到具体的实例
        }
    }
}
