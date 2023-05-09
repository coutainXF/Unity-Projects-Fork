using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFM.Player.PlayerStates
{
//如果边移动边射击就会进入这个状态。
    [CreateAssetMenu(menuName = "StateMachine/PlayerState/MoveFire", fileName = "PlayerState_MoveFire")]
    public class PlayerState_MoveFire : PlayerState
    {
        [SerializeField] float moveFireSpeed = 4f;
        [SerializeField] AudioData[] fireAudio;

        public override void Enter()
        {
            base.Enter();//播放开火动画
            //生成射弹
            _playerController.Fire(ShootPosition.Stand);
            AudioManager.Instance.PlaySFX(fireAudio);
        }
        
        public override void LogicUpdate()
        {
            if (StateDuration > .1f)
            {
                _stateMachine.SwitchState(typeof(PlayerState_Move));
            }
        }

        public override void PhysicUpdate()
        {
            _playerController.Move(moveFireSpeed);
        }

    }
}