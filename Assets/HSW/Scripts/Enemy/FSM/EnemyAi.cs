using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public EnemyStateMachine stateMachine;
    
    public Transform Player;
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float moveSpeed = 5f;
    public float attackRange = 5f;
    public float fireRate = 1f;
    
    public float lastShotTime;

    private void Awake()
    {
        stateMachine = new EnemyStateMachine();
        
    }
}
