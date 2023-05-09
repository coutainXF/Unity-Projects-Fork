using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected Vector3 projectileDirection;
    [SerializeField] protected float projectileSpeed;
    
    void Start()
    {
        transform.parent = GameObject.FindWithTag("ProjectileManager").transform;
        StartCoroutine(nameof(MoveProjectile));
        StartCoroutine(nameof(DestoryProjectile));
    }

    protected virtual IEnumerator MoveProjectile()
    {
        while (gameObject.activeSelf)
        {
            transform.Translate(projectileDirection * projectileSpeed * Time.deltaTime);
            yield return null;
        }
    }

    protected virtual IEnumerator DestoryProjectile()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.gameObject.name);
    }
}