using System.Collections;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    [SerializeField] GameObject vfxHit;
    [SerializeField] AudioData[] hitSfx;//击中时的音效
    PlayerController _playerController;
    Vector3 playerFacing;
    void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    protected override IEnumerator MoveProjectile()
    { 
        playerFacing = _playerController.gameObject.transform.localScale;
        while (gameObject.activeSelf)
        {
            transform.Translate(  playerFacing.x * projectileDirection * projectileSpeed * Time.deltaTime);
            transform.localScale = new Vector3(playerFacing.x,1,1);
            yield return null;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            // enemy.TakeDamage(xxx);
            enemy.TakeDamage(1);
            //播放声效
            AudioManager.Instance.PlaySFX(hitSfx);
            //生成一个hit动画预制件
            Instantiate(vfxHit ,col.GetContact(0).point,Quaternion.identity);
            //销毁射弹
            Destroy(gameObject);
        }

        if (col.gameObject.CompareTag("Obstacles"))
        {
            AudioManager.Instance.PlaySFX(hitSfx);
            //生成一个hit动画预制件
            Instantiate(vfxHit ,col.GetContact(0).point,Quaternion.identity);
            //销毁射弹
            Destroy(gameObject);
        }
    }
}