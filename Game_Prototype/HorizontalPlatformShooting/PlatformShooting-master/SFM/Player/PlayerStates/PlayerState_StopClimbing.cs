using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SFM.Player.PlayerStates
{
    [CreateAssetMenu(menuName = "StateMachine/PlayerState/StopClimbing", fileName = "PlayerState_StopClimbing")]
    public class PlayerState_StopClimbing : PlayerState
    {
        public override void Enter()
        {
            base.Enter();
            _animator.speed = 0;
            _playerController.SetVelocityY(0);
            _playerController.GetComponent<Rigidbody2D>().gravityScale = 0;
        }

        public override void LogicUpdate()
        {
            if (Mathf.Abs(_playerInput.AxisY) > 0)
            {
                _stateMachine.SwitchState(typeof(PlayerState_Climbing));
            }
            if (_playerInput.Jump)
            {
                _stateMachine.SwitchState(typeof(PlayerState_Idle));
            }
        }

        public override void PhysicUpdate()
        {
            
        }

        public override void Exit()
        {
            _animator.speed = 1;
            _playerController.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
}