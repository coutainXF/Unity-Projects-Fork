using System.Collections;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    [SerializeField] GameObject vfxHit;
    [SerializeField] AudioData[] hitSfx;
    
    protected override IEnumerator MoveProjectile()
    {
        while (gameObject.activeSelf)
        {
            float EnemyFacing = transform.localScale.x;
            transform.Translate(  EnemyFacing * projectileDirection * projectileSpeed * Time.deltaTime);
            yield return null;
        }
    }
    
    protected override void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent<PlayerController>(out PlayerController player))
        {
            // enemy.TakeDamage(xxx);
            player.TakeDamage(1);
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