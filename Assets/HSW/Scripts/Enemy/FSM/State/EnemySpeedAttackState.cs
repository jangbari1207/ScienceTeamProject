using UnityEngine;

public class EnemySpeedAttackState : EnemyState
{
    public EnemySpeedAttackState(AbstractEnemy enemy, EnemyStateMachine stateMachine, string aniBoolName) : base(enemy, stateMachine, aniBoolName)
    {
    }

    public override void UpdateState()
    {
        if (PlayerHealth.Instance == null || PlayerHealth.Instance.IsDead)
            return;
        Vector2 dir = enemy.Player.position - enemy.transform.position;
        enemy.RenderCompo.Flipx(dir.x);

        if (dir.magnitude > enemy.attackRange)
        {
            stateMachine.ChangeState(EnemyStateType.Chase);
        }
    }
    public override void AniFireTrigger()
    {
        Shoot();
    }
    private void Shoot()
    {
        //SoundManager.Instance.PlaySFX(enemy.normalgunSound);
        Vector2 dir =
            (enemy.Player.position - enemy.firePoint.position).normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle -= 90f;

        GameObject bullet = Object.Instantiate(
            enemy.bulletPrefab,
            enemy.firePoint.position,
            Quaternion.Euler(0, 0, angle));

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = dir * 8f;
    }
}
