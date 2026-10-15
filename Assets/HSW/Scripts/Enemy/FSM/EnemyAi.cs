using UnityEngine;

public class EnemyAI : MonoBehaviour
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
        stateMachine.AddState(EnemyStateType.Chase, new EnemyChaseState(this, stateMachine));
        stateMachine.AddState(EnemyStateType.Attack, new EnemyAttackState(this, stateMachine));
        
        stateMachine.Initialize(EnemyStateType.Chase);
    }

    private void Update()
    {
        stateMachine.CurrentState.UpdateState();
    }
}
