using UnityEngine;

namespace SFM.Enemy.EnemyStates
{
    public class EnemyState_Idle : EnemyState
    {
        public override void Enter()
        {
            stateHash = _stateMachine.stateAnim[typeof(EnemyState_Idle)];
            base.Enter();//父类播放了动画
        }

        public override void LogicUpdate()
        {
            if (_enemy.transform.position.y < -20)
            {
                _stateMachine.SwitchState(typeof(EnemyState_Death));
            }
            if (StateDuration > 1f)
            {
                _stateMachine.SwitchState(typeof(EnemyState_Patrol));
            }
            if (_enemy.IsDeath)
            {
                _stateMachine.SwitchState(typeof(EnemyState_Death));
            }
            if (_enemyDetective.isPlayerInSight)
            {
                _stateMachine.SwitchState(typeof(EnemyState_Fire));
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