using System;
using System.Collections.Generic;
using SFM.Enemy.EnemyStates;
using State_Machine_System.Base;
using UnityEngine;

[Serializable]
public class EnemyParamter
{
    public string[] stateName;
    public float transitionDuration = 0.1f;
    public int scorePoint;//击杀敌人得分
    public AudioData deathSFX;//死亡音效
    public AudioData hitSFX;//受击音效
    public AudioData shootSFX;//射击音效
}
public class EnemyStateMachine : StateMachine
{
    [SerializeField] EnemyParamter paramter;
    EnemyState[] states;
    Animator _animator;
    EnemyDetective _detective;

    EnemyController _enemyController;
    Enemy _enemy;
    
    public Dictionary<System.Type, int> stateAnim;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _detective = GetComponent<EnemyDetective>();
        _enemyController = GetComponent<EnemyController>();
        _enemy = GetComponent<Enemy>();
        
        states = new EnemyState[]
        {
            new EnemyState_Death(),
            new EnemyState_Fire(),
            new EnemyState_Idle(),
            new EnemyState_Jump(),
            new EnemyState_Patrol()
        };
        
        stateTable = new Dictionary<System.Type, IState>(states.Length);
        stateAnim = new Dictionary<Type, int>(states.Length);

        int i = 0;
        foreach (var state in states)
        {
            state.Initialize(_animator,_detective,this,
                _enemyController,_enemy,paramter);
            stateTable.Add(state.GetType(),state);
            
            //初始化动画表
            stateAnim.Add(state.GetType(),Animator.StringToHash(paramter.stateName[i++]));
        }
    }

    private void Start()
    {
        SwitchOn(stateTable[typeof(EnemyState_Idle)]);//根据类型从字典中去到具体的实例
    }

}