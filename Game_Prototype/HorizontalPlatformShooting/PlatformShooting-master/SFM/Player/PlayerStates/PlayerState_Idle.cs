using UnityEditorInternal;
using UnityEngine;

namespace SFM.Player.PlayerStates
{
    [CreateAssetMenu(menuName = "StateMachine/PlayerState/Idle",fileName = "PlayerState_Idle")]
    public class PlayerState_Idle : PlayerState
    {
        public override void Enter()
        {
            base.Enter();//父类播放了动画
            _playerController.SetVelocityX(0);//让玩家停止移动
        }

        public override void LogicUpdate()
        {
            if (_playerInput.Move)
            {
                _stateMachine.SwitchState(typeof(PlayerState_Move));
            }

            if (_playerInput.Fire)
            {
                _stateMachine.SwitchState(typeof(PlayerState_RifleFire));
            }

            if (_playerInput.Jump && _playerController.isGrounded)
            {
                _stateMachine.SwitchState(typeof(PlayerState_Jump));
            }

            if (_playerController.CanClimb && Mathf.Abs(_playerInput.AxisY) > 0)//如果玩家上下移动的输入大于零并且在梯子附近
            {
                _stateMachine.SwitchState(typeof(PlayerState_Climbing));
            }
            
            if (_playerInput.isProning)//如果玩家键入向下的输入，则趴下
            {
                _stateMachine.SwitchState(typeof(PlayerState_Prone));
            }
        }

        public override void PhysicUpdate()
        {
            
        }

        public override void Exit()
        {
            
            
        }
    }
}