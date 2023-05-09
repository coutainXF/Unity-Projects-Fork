using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;


namespace SFM.Player.PlayerStates
{
    [CreateAssetMenu(menuName = "StateMachine/PlayerState/Climbing", fileName = "PlayerState_Climbing")]
    public class PlayerState_Climbing : PlayerState
    {
        [SerializeField] float climbSpeed = 4f;
        public override void Enter()
        {
            base.Enter();//播放动画
            _playerController.SetVelocityX(0);
            if (!_playerController.IsClimbing)//进入攀爬梯子状态时
            {
                //修正玩家在梯子上的位置
                var positionX = _playerController.ladderPlayerClimb.transform.position.x;
                var playerPos = _playerController.transform.position;
                _playerController.transform.position = new Vector3(positionX,playerPos.y,playerPos.z);//修正位置
                _playerController.IsClimbing = true;
            }
            //_playerController.ladderPlayerClimb.gameObject.transform
        }

        public override void LogicUpdate()
        {
            if (_playerInput.AxisY == 0)
            {
                _stateMachine.SwitchState(typeof(PlayerState_StopClimbing));
            }
            if (_playerController.CanClimb == false)
            {
                _stateMachine.SwitchState(typeof(PlayerState_Idle));
            }

            if (_playerInput.Jump)
            {
                _stateMachine.SwitchState(typeof(PlayerState_Idle));
            }
        }

        public override void PhysicUpdate()
        {
            _playerController.SetVelocityY(_playerInput.AxisY * climbSpeed);
        }

        public override void Exit()
        {
            
        }
    }
}