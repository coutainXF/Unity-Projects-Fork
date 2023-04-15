using UnityEngine;

namespace SFM.Player.PlayerStates
{
    [CreateAssetMenu(menuName = "StateMachine/PlayerState/Move",fileName = "PlayerState_Move")]
    public class PlayerState_Move : PlayerState
    {
        [SerializeField] float moveSpeed = 5f;
        public override void Enter()
        {
            base.Enter();//父类播放了动画
        }

        public override void LogicUpdate()
        {
            if (_playerInput.Fire)
            {
                _stateMachine.SwitchState(typeof(PlayerState_MoveFire));
            }
            if (!_playerInput.Move)
            {
                _stateMachine.SwitchState(typeof(PlayerState_Idle));
            }
            if (_playerController.CanClimb && Mathf.Abs(_playerInput.AxisY) > 0)//如果玩家上下移动的输入大于零并且在梯子附近
            {
                _stateMachine.SwitchState(typeof(PlayerState_Climbing));
            }
            if (_playerInput.isProning)//如果玩家键入向下的输入，则趴下
            {
                _stateMachine.SwitchState(typeof(PlayerState_Prone));
            }
            if (_playerInput.Jump && _playerController.isGrounded)
            {
                _stateMachine.SwitchState(typeof(PlayerState_Jump));
            }
            
        }

        public override void PhysicUpdate()
        {
            _playerController.Move(moveSpeed);
        }

        public override void Exit()
        {
            
            
        }
    }
}