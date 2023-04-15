using UnityEngine;
public class PlayerGroundedDetective : MonoBehaviour
{
    [SerializeField] float detectionRadius = 0.1f;
    [SerializeField] LayerMask groundLayer;
    private Collider2D[] _colliders = new Collider2D[1];
    public bool isGround => Physics2D.OverlapCircleNonAlloc(transform.position,detectionRadius,_colliders,groundLayer) != 0;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,detectionRadius);
    }
}