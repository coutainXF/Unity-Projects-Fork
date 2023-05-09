using Input;
using UnityEngine;

public enum ShootPosition
{
    Stand,
    Prone
}

public class PlayerController : Character
{

    [SerializeField] Transform muzzle;
    [SerializeField] Transform muzzleProne;
    [SerializeField] GameObject playerProjectile;

    [SerializeField] AudioData damageSfx;
    
    Rigidbody2D _playerRigid;//获取玩家刚体
    PlayerInput _playerInput;//获取玩家输入
    PlayerGroundedDetective _groundedDetector;//获取玩家着地检测脚本
    public bool isGrounded => _groundedDetector.isGround;//指定是否着地
    public bool isFalling => _playerRigid.velocity.y < 0 && !isGrounded ;//当不在地面上且玩家的y轴速度小于零时，玩家处于下落状态

    public bool CanClimb = false;//标识是否可以爬行
    public bool IsClimbing = false;//标识是否正在爬行

    public Collider2D ladderPlayerClimb;//指定玩家攀爬的梯子
    
    void Awake()
    {
        _playerRigid = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
        _groundedDetector = GetComponentInChildren<PlayerGroundedDetective>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        AudioManager.Instance.PlaySFX(damageSfx);
        PlayerHpFiller.Instance.UpdatePlayerHp(curHealth,maxHealth);
    }

    public void Move(float speed)
    {
        if (_playerInput.Move)
        {
            transform.localScale = new Vector3(_playerInput.AxisX, 1f, 1f);
        }
        SetVelocityX(speed * _playerInput.AxisX);
    }
    
    public void SetVelocity(Vector3 speed)
    {
        _playerRigid.velocity = speed;
    }
    
    public void SetVelocityX(float speed)
    {
        _playerRigid.velocity = new Vector2(speed,_playerRigid.velocity.y);
    }
    
    public void SetVelocityY(float speed)
    {
        _playerRigid.velocity = new Vector2(_playerRigid.velocity.x,speed);
    }

    //玩家开火
    public void Fire(ShootPosition shootPosition)
    {
        if (shootPosition==ShootPosition.Stand)
        {
            var projectile = Instantiate(playerProjectile);
            projectile.transform.position = muzzle.position;
        }else if (shootPosition == ShootPosition.Prone)
        {
            var projectile =Instantiate(playerProjectile);
            projectile.transform.position = muzzleProne.position;
        }
    }
}