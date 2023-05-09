using Input;
using State_Machine_System.Base;
using UnityEngine;

namespace SFM.Player
{
    public class PlayerState : ScriptableObject,IState
    {
        [SerializeField] string stateName;
        [SerializeField,Range(0f,1f)] float transitionDuration = 0.1f;
        
        protected Animator _animator;
        protected PlayerInput _playerInput;
        protected PlayerStateMachine _stateMachine;
        protected PlayerController _playerController;
        protected CapsuleCollider2D _collider2D;

        
        protected int stateHash; 
        float stateStartTime;

        protected bool IsAnimationFinished => StateDuration >= _animator.GetCurrentAnimatorStateInfo(0).length;
        protected float StateDuration => Time.time - stateStartTime;
        
        
        public void Initialize(Animator animator, PlayerInput playerInput, PlayerController playerController,
            PlayerStateMachine stateMachine, CapsuleCollider2D collider2D)
        {
            this._playerController = playerController; 
            this._playerInput = playerInput;
            this._animator = animator;
            this._stateMachine = stateMachine;
            this._collider2D = collider2D;
        }
        void OnEnable()
        {
            stateHash = Animator.StringToHash(stateName);
        }

        public virtual void Enter()
        {
            _animator.CrossFade(stateHash,transitionDuration);//通过动画哈希播放动画
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
}
