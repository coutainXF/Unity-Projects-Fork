using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject enemyProjectile;//敌人射弹
    [SerializeField] Transform muzzle;
    
    Rigidbody2D enemyRigid;//敌人刚体组件
    Vector3 targetPos;//巡逻目的地
    Vector2 direction;
    Coroutine fireCoroutine;
    void Awake()
    {
        enemyRigid = GetComponent<Rigidbody2D>();
    }
    public void Move(float speed)
    {
        transform.localScale = new Vector3(speed >=0 ? 1 : -1, 1f, 1f);
        SetVelocityX(speed);
    }
    public void SetVelocity(Vector3 speed)
    {
        enemyRigid.velocity = speed;
    }
    public void SetVelocityX(float speed)
    {
        enemyRigid.velocity = new Vector2(speed,enemyRigid.velocity.y);
    }
    public void SetVelocityY(float speed)
    {
        enemyRigid.velocity = new Vector2(enemyRigid.velocity.x,speed);
    }
    public void FireOneShot()
    {
        var projectile = Instantiate(enemyProjectile);
        projectile.transform.position = muzzle.position;
        projectile.transform.localScale = transform.localScale;
    }
    private IEnumerator ContinuouslyFire(int times,float fireInterval)
    {
        while(times > 0)
        {
            yield return new WaitForSeconds(fireInterval);
            FireOneShot();
            times--;
        }
    }

    public void FireContinuously(int times,float fireInterval)
    {
        if (fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
        }
        fireCoroutine = StartCoroutine(ContinuouslyFire(times,fireInterval));
    }
}