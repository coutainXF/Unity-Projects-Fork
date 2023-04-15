using UnityEngine;

namespace SFM.Enemy.EnemyStates
{
    public class EnemyState_Patrol : EnemyState
    {
        [SerializeField] float moveSpeed = 4f;

        public override void Enter()
        {
            stateHash = _stateMachine.stateAnim[typeof(EnemyState_Patrol)];
            base.Enter();
            if (_enemy._enemyClass == EnemyClass.Patrol)
            {
                moveSpeed = -moveSpeed;
            }else if (_enemy._enemyClass == EnemyClass.RunToLeft)
            {
                moveSpeed = -Mathf.Abs(moveSpeed);
            }
        }

        public override void LogicUpdate()
        {
            if (_enemy.transform.position.y < -20)
            {
                _stateMachine.SwitchState(typeof(EnemyState_Death));
            }
            if (_enemyDetective.isColliding)
            {
                _stateMachine.SwitchState(typeof(EnemyState_Jump));
            }
            if (_enemy.IsDeath)
            {
                _stateMachine.SwitchState(typeof(EnemyState_Death));
            }

            if (_enemyDetective.isHoleBefore && StateDuration > .2f)
            {
                _stateMachine.SwitchState(typeof(EnemyState_Jump));
            }

            if (_enemyDetective.isPlayerInSight)
            {
                _stateMachine.SwitchState(typeof(EnemyState_Fire));
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
            _enemyController.Move(moveSpeed);
        }

        public override void Exit()
        {
            
        }
    }
}