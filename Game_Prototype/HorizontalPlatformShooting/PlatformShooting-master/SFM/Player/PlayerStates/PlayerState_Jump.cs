using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SFM.Player.PlayerStates
{
    [CreateAssetMenu(menuName = "StateMachine/PlayerState/Jump", fileName = "PlayerState_Jump")]
    public class PlayerState_Jump : PlayerState
    {
        [SerializeField] float jumpSpeed = 2f;
        [SerializeField] float speed = 4f;//空中的移动速度
        public override void Enter()
        {
            base.Enter();
            _playerController.SetVelocityY(jumpSpeed);
        }

        public override void LogicUpdate()
        {
            if (_playerController.isFalling)
            {
                _stateMachine.SwitchState(typeof(PlayerState_Fall));
            }
            if (_playerController.CanClimb && Mathf.Abs(_playerInput.AxisY) > 0)//如果玩家上下移动的输入大于零并且在梯子附近
            {
                _stateMachine.SwitchState(typeof(PlayerState_Climbing));
            }
            if (IsAnimationFinished)
            {
                _stateMachine.SwitchState(typeof(PlayerState_Idle));
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