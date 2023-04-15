using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SFM.Player.PlayerStates
{
    [CreateAssetMenu(menuName = "StateMachine/PlayerState/Fall", fileName = "PlayerState_Fall")]
    public class PlayerState_Fall : PlayerState
    {
        [SerializeField] float speed = 4f;
        public override void Enter()
        {
            base.Enter();
        }

        public override void LogicUpdate()
        {
            if (IsAnimationFinished)
            {
                _stateMachine.SwitchState(typeof(PlayerState_Idle));
            }
            if (_playerInput.isProning)//如果玩家键入向下的输入，则趴下
            {
                _stateMachine.SwitchState(typeof(PlayerState_Prone));
            }
            if (_playerController.CanClimb && Mathf.Abs(_playerInput.AxisY) > 0)//如果玩家上下移动的输入大于零并且在梯子附近
            {
                _stateMachine.SwitchState(typeof(PlayerState_Climbing));
            }
            if (_playerController.isGrounded && _playerInput.Jump)
            {
                _stateMachine.SwitchState(typeof(PlayerState_Jump));
            }
        }

        public override void PhysicUpdate()
        {
            if (_playerInput.Move)
            {
                _playerController.Move(speed);
            }
        }

        public override void Exit()
        {
            
        }
    }
}