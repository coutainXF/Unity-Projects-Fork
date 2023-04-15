
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using State_Machine_System.Base;
using UnityEngine;


public class EnemyState : IState
{
    protected EnemyParamter _paramter;
    protected Animator _animator;
    protected EnemyStateMachine _stateMachine;
    protected EnemyDetective _enemyDetective;
    protected EnemyController _enemyController;
    protected Enemy _enemy;
    
    protected int stateHash;
    
    
    float stateStartTime;

    protected bool IsAnimationFinished => StateDuration >= _animator.GetCurrentAnimatorStateInfo(0).length;
    protected float StateDuration => Time.time - stateStartTime;
    
    public void Initialize(Animator animator,EnemyDetective enemyDetective,
        EnemyStateMachine stateMachine,EnemyController enemyController,Enemy enemy,EnemyParamter paramter)
    {
        this._animator = animator;
        this._enemyDetective = enemyDetective;
        this._stateMachine = stateMachine;
        this._enemyController = enemyController;
        this._enemy = enemy;
        this._paramter = paramter;
    }
    
    public virtual void Enter()
    {
        _animator.CrossFade(stateHash,_paramter.transitionDuration);//通过动画哈希播放动画
        stateStartTime = Time.time;
    }

    public virtual void Exit()
    {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicUpdate()
    {
            
    }
}
