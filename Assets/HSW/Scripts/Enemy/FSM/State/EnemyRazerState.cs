using UnityEngine;

public class EnemyRazerState : EnemyState
{
    [Header("Laser")]
    [SerializeField] private GameObject previewPrefab;

    private float aimDuration = 1f;
    private float attackInterval = 3f;

    private float aimTimer;
    private float cooldownTimer;

    private GameObject previewObject;
    private bool isAiming;

    public EnemyRazerState(
        AbstractEnemy enemy,
        EnemyStateMachine stateMachine,
        string aniBoolName
    ) : base(enemy, stateMachine, aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        aimTimer = 0f;
        cooldownTimer = 0f;
        isAiming = false;

        DestroyPreview();
    }

    public override void UpdateState()
    {
        if (PlayerHealth.Instance == null || PlayerHealth.Instance.IsDead)
            return;
        if (enemy.Player == null || enemy.firePoint == null)
            return;

        Vector2 direction =
            (Vector2)enemy.Player.position -
            (Vector2)enemy.firePoint.position;

        enemy.RenderCompo.Flipx(direction.x);

        float distance = Vector2.Distance(
            enemy.transform.position,
            enemy.Player.position
        );
        
        if (distance > enemy.attackRange)
        {
            DestroyPreview();
            stateMachine.ChangeState(EnemyStateType.Chase);
            return;
        }
        
        if (!isAiming)
        {
            cooldownTimer += Time.deltaTime;

            if (cooldownTimer >= attackInterval)
                StartAim(direction);

            return;
        }
        
        UpdatePreview(direction);

        aimTimer += Time.deltaTime;

        if (aimTimer >= aimDuration)
        {
            FireLaser(direction.normalized);

            DestroyPreview();

            isAiming = false;
            aimTimer = 0f;
            cooldownTimer = 0f;
        }
    }

    private void StartAim(Vector2 direction)
    {
        if (enemy.previewPrefab == null)
        {
            Debug.LogError("조준용 Preview Prefab이 연결되지 않았습니다.");
            return;
        }

        previewObject = Object.Instantiate(
            enemy.previewPrefab,
            enemy.firePoint.position,
            Quaternion.identity
        );

        isAiming = true;
        aimTimer = 0f;

        UpdatePreview(direction);
    }

    private void UpdatePreview(Vector2 direction)
    {
        if (previewObject == null)
            return;

        previewObject.transform.position = enemy.firePoint.position;

        float angle =
            Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        previewObject.transform.rotation =
            Quaternion.Euler(0f, 0f, angle);
    }

    private void FireLaser(Vector2 direction)
    {
        if (enemy.bulletPrefab == null || enemy.firePoint == null)
        {
            Debug.LogError("Laser Prefab 또는 FirePoint가 연결되지 않았습니다.");
            return;
        }

        float angle =
            Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        GameObject laser = Object.Instantiate(
            enemy.bulletPrefab,
            enemy.firePoint.position,
            Quaternion.Euler(0f, 0f, angle)
        );
        
        LaserProjectile projectile = laser.GetComponent<LaserProjectile>();

        if (projectile != null)
            projectile.enabled = false;
    }

    private void DestroyPreview()
    {
        if (previewObject != null)
            Object.Destroy(previewObject);

        previewObject = null;
        isAiming = false;
        aimTimer = 0f;
    }

    public override void Exit()
    {
        base.Exit();
        DestroyPreview();
    }
}