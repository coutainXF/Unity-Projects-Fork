using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] float boomForce = 5f;
    [SerializeField] AudioData[] boomSfx;
    [SerializeField] GameObject boomHit;
    RaycastHit2D[] groundDetective = new RaycastHit2D[1];
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<PlayerController>(out PlayerController player))
        {
            AudioManager.Instance.PlaySFX(boomSfx);
            // enemy.TakeDamage(xxx);
            player.TakeDamage(3);
            //提供一个爆炸力
            player.SetVelocityY(boomForce);
            //生成一个hit动画预制件
            Instantiate(boomHit,transform.position,Quaternion.identity);
            //销毁射弹
            Destroy(gameObject);
        }

        if (col.gameObject.TryGetComponent<PlayerProjectile>(out PlayerProjectile projectile))
        {
            AudioManager.Instance.PlaySFX(boomSfx);
            Destroy(projectile.gameObject);
            Destroy(gameObject);
        }
    }
}
