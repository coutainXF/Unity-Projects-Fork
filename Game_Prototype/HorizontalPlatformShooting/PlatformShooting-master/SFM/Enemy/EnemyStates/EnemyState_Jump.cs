using UnityEngine;

namespace SFM.Enemy.EnemyStates
{
    public class EnemyState_Jump : EnemyState
    {
        float jumpForce = 6f;
        float moveSpeed = 4f;
        public override void Enter()
        {
            stateHash = _stateMachine.stateAnim[typeof(EnemyState_Jump)];
            base.Enter();//父类播放了动画
            _enemyDetective.isColliding = false;
            _enemyController.SetVelocityY(jumpForce);
        }

        public override void LogicUpdate()
        {
            if (StateDuration > .4f)
            {
                _enemyController.SetVelocityX(_enemyController.transform.localScale.x==1 ? moveSpeed : -moveSpeed);
            }
            if (_enemyDetective.isGround && StateDuration > 1f)
            {
                _stateMachine.SwitchState(typeof(EnemyState_Idle));
            }
            
            if (_enemy.IsDeath)
            {
                _stateMachine.SwitchState(typeof(EnemyState_Death));
            }

            if (StateDuration > 2f)
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
        }

        public override void PhysicUpdate()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}