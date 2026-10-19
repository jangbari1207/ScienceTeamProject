
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform player;
    [SerializeField] private string playerTag = "Player";

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float stopDistance = 2f;

    [Header("Attack")]
    [SerializeField] private float attackRange = 2.5f;
    [SerializeField] private float attackCooldown = 1f;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private float nextAttackTime;
    private bool isAttacking;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        FindPlayer();

        if (player == null)
            Debug.LogError("BasicEnemy: Player 태그를 가진 오브젝트를 찾지 못했습니다.", this);
    }

    private void Update()
    {
        if (player == null)
        {
            FindPlayer();
            return;
        }

        float distance = Vector2.Distance(
            transform.position,
            player.position
        );

        // 플레이어가 있는 방향으로 스프라이트 바라보기
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX =
                player.position.x < transform.position.x;
        }

        // 공격 범위 안이면 멈추고 공격
        if (distance <= attackRange)
        {
            if (animator != null)
                animator.SetFloat("Speed", 0f);

            if (!isAttacking && Time.time >= nextAttackTime)
                Attack();

            return;
        }

        // 이동 애니메이션
        if (animator != null)
            animator.SetFloat("Speed", 1f);
    }

    private void FixedUpdate()
    {
        if (player == null || isAttacking || rb == null)
            return;

        Vector2 targetPosition = player.position;
        Vector2 currentPosition = rb.position;

        float distance = Vector2.Distance(
            currentPosition, targetPosition);

        if (distance <= attackRange || distance <= stopDistance)
            return;

        Vector2 direction =
            (targetPosition - currentPosition).normalized;

        rb.MovePosition(
            currentPosition + direction * moveSpeed * Time.fixedDeltaTime
        );
    }

    private void FindPlayer()
    {
        GameObject playerObject =
            GameObject.FindGameObjectWithTag(playerTag);

        if (playerObject != null)
            player = playerObject.transform;
    }

    private void Attack()
    {
        isAttacking = true;
        nextAttackTime = Time.time + attackCooldown;

        if (animator != null)
            animator.SetTrigger("Attack");

        Debug.Log("적 공격 시작!");

        // 기본 공격 테스트용: 잠시 후 다시 이동 가능
        Invoke(nameof(EndAttack), 0.3f);
    }

    private void EndAttack()
    {
        isAttacking = false;
    }
}
