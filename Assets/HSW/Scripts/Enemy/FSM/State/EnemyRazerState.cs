using UnityEngine;
public class EnemyRazerState : EnemyState
{
    private GameObject previewObject;

    private bool fired;
    public EnemyRazerState(AbstractEnemy enemy, EnemyStateMachine stateMachine, string aniBoolName)

    : base(enemy, stateMachine, aniBoolName) { }
    public override void Enter()
    {
        base.Enter();
        fired = false;
        StartAim();
    }

    public override void UpdateState()
    {
        if (PlayerHealth.Instance == null || PlayerHealth.Instance.IsDead) return;

        if (enemy.Player == null || enemy.firePoint == null) return;

        // 발사 전까지만 플레이어를 따라 조준
        if (!fired)
        {
            Vector2 dir = (Vector2)enemy.Player.position - (Vector2)enemy.firePoint.position;

            enemy.RenderCompo.Flipx(dir.x);

            UpdatePreview(dir);
        }

    }

    // 애니메이션 이벤트: AnimationFire
    public override void AniFireTrigger()
    {
        if (fired) return;
        fired = true;
        SoundManager.Instance.PlaySFX(enemy.laserSound);

        Vector2 dir = (Vector2)enemy.Player.position - (Vector2)enemy.firePoint.position;

        DestroyPreview();

        FireLaser(dir.normalized);
    }

    // 애니메이션 이벤트: AnimationEnd
    public override void AniEndTrigger()
    {
        base.AniEndTrigger();

        enemy.lastShotTime = Time.time; // 쿨타임 시작

        stateMachine.ChangeState(EnemyStateType.Chase);
    }

    private void StartAim()
    {
        if (enemy.previewPrefab == null) { Debug.LogError("Preview Prefab 미연결"); return; }

        previewObject = Object.Instantiate(enemy.previewPrefab, enemy.firePoint.position, Quaternion.identity);
    }
    private void UpdatePreview(Vector2 dir)
    {
        if (previewObject == null) return;

        previewObject.transform.position = enemy.firePoint.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        previewObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
    private void FireLaser(Vector2 dir)
    {
        if (enemy.bulletPrefab == null) { Debug.LogError("Laser Prefab 미연결"); return; }

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        GameObject laser = Object.Instantiate(enemy.bulletPrefab, enemy.firePoint.position, Quaternion.Euler(0f, 0f, angle));

        LaserProjectile projectile = laser.GetComponent<LaserProjectile>();
        if (projectile != null) projectile.enabled = false;
    }
    private void DestroyPreview()
    {
        if (previewObject != null) Object.Destroy(previewObject);
        previewObject = null;
    }
    public override void Exit()
    {
        base.Exit();
        DestroyPreview(); // 피격 등으로 중간에 끊겨도 조준선 정리
    }
}