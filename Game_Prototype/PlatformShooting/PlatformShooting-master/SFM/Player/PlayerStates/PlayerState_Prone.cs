using UnityEngine;

namespace SFM.Player.PlayerStates
{
    [CreateAssetMenu(menuName = "StateMachine/PlayerState/Prone",fileName = "PlayerState_Prone")]
    public class PlayerState_Prone : PlayerState
    {
        [Header("趴下时的碰撞体信息")]
        [SerializeField] Vector2 colliderBounds;                //趴下时碰撞体的大小
        [SerializeField] Vector2 colliderOffset;                //趴下时碰撞体的偏移
        [SerializeField] CapsuleDirection2D direction;

        Vector2 colliderBoundsStand = new Vector2(.6f,2.3f);//站立时碰撞体的大小(.6f,2.3f)
        Vector2 colliderOffsetStand = new Vector2(0f,1.2f);//站立时碰撞体的大小(0f,1.2f)

        
        public override void Enter()
        {
            base.Enter();//父类播放动画
            _playerController.SetVelocityX(0f);
            //更新碰撞体信息为下趴时的信息
            _collider2D.direction = direction;
            _collider2D.size = colliderBounds;
            _collider2D.offset = colliderOffset;
        }

        public override void LogicUpdate()
        {
            if (_playerInput.Fire)
            {
                _stateMachine.SwitchState(typeof(PlayerState_ProneFire));
            }
            if (!_playerInput.isProning)
            {
                _collider2D.direction = CapsuleDirection2D.Vertical;
                _collider2D.size = colliderBoundsStand;
                _collider2D.offset = colliderOffsetStand;
                _stateMachine.SwitchState(typeof(PlayerState_Idle));
            }
            if (_playerController.CanClimb && _playerInput.AxisY > 0)//如果玩家在梯子附近往上爬梯
            {
                _collider2D.direction = CapsuleDirection2D.Vertical;
                _collider2D.size = colliderBoundsStand;
                _collider2D.offset = colliderOffsetStand;
                if (!_playerController.isGrounded)
                {
                    _stateMachine.SwitchState(typeof(PlayerState_Climbing));
                }
            }
            if (_playerController.CanClimb && _playerInput.AxisY < 0 && !_playerController.isGrounded)//如果玩家在梯子附近下爬梯
            {
                _collider2D.direction = CapsuleDirection2D.Vertical;
                _collider2D.size = colliderBoundsStand;
                _collider2D.offset = colliderOffsetStand;
                if (!_playerController.isGrounded)
                {
                    _stateMachine.SwitchState(typeof(PlayerState_Climbing));
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