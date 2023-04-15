using System;
using System.Collections;
using UnityEngine;


//根据检测返回结果，控制状态机
public class EnemyDetective : MonoBehaviour
{
    [Header("检测玩家")]
    [SerializeField] Vector2 size;
    [SerializeField] Vector3 offset;
    [SerializeField] LayerMask targetLayer;

    [Header("检测空洞")] 
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Vector3 rayoffset;
    [SerializeField] float rayLenth;

    [Header("着地检测")] 
    [SerializeField] float detectionRadius =.2f;
    [SerializeField] Vector3 groundOffset = new Vector3(0,2,0);
    Collider2D[] _colliders = new Collider2D[1];
    
    RaycastHit2D[] raycastHit2D = new RaycastHit2D[1];
    Collider2D[] collider2D = new Collider2D[1];

    [Header("检测碰壁")] 
    public bool isColliding = false;

    bool FacingRight = true;//面朝右？
    Vector3 pos;
    Vector3 sight;
    
    public bool isPlayerInSight => FacingRight
        ? Physics2D.OverlapBoxNonAlloc(transform.position+offset,size,0,collider2D,targetLayer)!=0
        : Physics2D.OverlapBoxNonAlloc(sight,size,0,collider2D,targetLayer)!=0;

    public bool isHoleBefore =>FacingRight
        ? Physics2D.RaycastNonAlloc(transform.position + rayoffset, Vector2.down,raycastHit2D ,rayLenth, groundLayer) == 0
        : Physics2D.RaycastNonAlloc(pos, Vector2.down,raycastHit2D ,rayLenth, groundLayer) == 0;

    public bool isGround => Physics2D.OverlapCircleNonAlloc(transform.position + groundOffset, detectionRadius, _colliders, groundLayer) != 0;

    void OnEnable()
    {
        StartCoroutine(nameof(CheckFacing));
    }

    void OnDisable()
    {
        StopCoroutine(nameof(CheckFacing));
    }
    
    IEnumerator CheckFacing()
    {
        while (gameObject.activeSelf)
        {
            FacingRight = transform.localScale==Vector3.one;
            yield return null;
        }
    }
    
    void OnDrawGizmos()
    {
        pos = transform.position;
        pos.x = pos.x - rayoffset.x;

        sight = transform.position;
        sight.x = sight.x - offset.x;
        sight.y = transform.position.y+offset.y;
        
        if (FacingRight)
        {
            Gizmos.DrawWireCube(transform.position+offset,size);
            Gizmos.DrawRay(transform.position+rayoffset,Vector3.down);
        }
        else
        {
            Gizmos.DrawWireCube((sight),size);
            Gizmos.DrawRay((pos),Vector3.down);
        }
        
        Gizmos.DrawWireSphere((transform.position+groundOffset),detectionRadius);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacles"))
        {
            isColliding = true;
        }
    }
}