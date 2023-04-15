using UnityEngine;

namespace SFM.Enemy.EnemyStates
{
    public class EnemyState_Death : EnemyState
    {
        public override void Enter()
        {
            stateHash = _stateMachine.stateAnim[typeof(EnemyState_Death)];
            base.Enter();
            AudioManager.Instance.PlaySFX(_paramter.deathSFX);
            ScoreManager.Instance.AddScore(_paramter.scorePoint);
            ScoreManager.Instance.AddCombo(1);
        }

        public override void LogicUpdate()
        {
            if (IsAnimationFinished)
            {
                _enemy.gameObject.SetActive(false);
                if (!_enemy.IsDeath)
                    _stateMachine.SwitchState(typeof(EnemyState_Idle));
            }
            
        }
    }
}