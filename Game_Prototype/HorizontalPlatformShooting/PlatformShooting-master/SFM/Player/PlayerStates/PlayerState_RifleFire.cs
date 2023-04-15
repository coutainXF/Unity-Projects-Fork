using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFM.Player.PlayerStates
{
//如果边移动边射击就会进入这个状态。
    [CreateAssetMenu(menuName = "StateMachine/PlayerState/RifleFire", fileName = "PlayerState_RifleFire")]
    public class PlayerState_RifleFire : PlayerState
    {
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
                _stateMachine.SwitchState(typeof(PlayerState_Idle));
            }
        }

        public override void PhysicUpdate()
        {
            
        }
    }
}