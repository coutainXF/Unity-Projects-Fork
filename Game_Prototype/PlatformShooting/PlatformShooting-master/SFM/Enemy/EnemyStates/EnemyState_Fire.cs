using UnityEngine;

namespace SFM.Enemy.EnemyStates
{
    public class EnemyState_Fire : EnemyState
    {
        [Range(0,100)] int randomFire = 20;//敌人在连续开火和单次开火之间切换
        int fireTimes = 3;//连续开火次数
        float fireInterval = .5f;//连续开火间隔
        float fireTime = 2f;//开火后呆住的时间
        int range;
        
        public override void Enter()
        {
            stateHash = _stateMachine.stateAnim[typeof(EnemyState_Fire)];
            base.Enter();//父类播放了动画
            _enemyController.SetVelocityX(0);
            int range = Random.Range(0, 100);
            if (randomFire > range)
            {
                _enemyController.FireContinuously(fireTimes, fireInterval);
            }
            else
            {
                _enemyController.FireOneShot();
            }
        }

        public override void LogicUpdate()
        {
            if (StateDuration > fireTime)
            {
                var i = Random.Range(0, 3);
                if (i == 0)
                {
                    _stateMachine.SwitchState(typeof(EnemyState_Patrol));
                }else if (i == 1)
                {
                    _stateMachine.SwitchState(typeof(EnemyState_Jump));
                }else if (i == 2)
                {
                    _stateMachine.SwitchState(typeof(EnemyState_Idle));
                }
            }
            if (_enemy.IsDeath)
            {
                _stateMachine.SwitchState(typeof(EnemyState_Death));
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