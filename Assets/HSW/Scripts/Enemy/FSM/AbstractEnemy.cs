using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour
{
    public Transform Player;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float moveSpeed = 5f;
    public float attackRange = 5f;
    public float fireRate = 1f;
    
    public float lastShotTime;

    public Animator AnimCompo { get; private set; }

    protected virtual void Awake()
    {
        AnimCompo = GetComponentInChildren<Animator>();
    }
    public virtual void AnimationEndTrigger()
    {

    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.purple;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
