using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(AbstractEnemy enemy, EnemyStateMachine stateMachine, string aniBoolName) : base(enemy, stateMachine, aniBoolName)
    {
    }

    public override void UpdateState()
    {
        Vector2 dir = enemy.Player.position - enemy.transform.position;
        enemy.RenderCompo.Flipx(dir.x);

        if (dir.magnitude > enemy.attackRange)
        {
            stateMachine.ChangeState(EnemyStateType.Chase);
        }
    }

    public override void AniFireTrigger()
    {
        if (Time.time < enemy.lastShotTime + enemy.fireRate) return;
        enemy.lastShotTime = Time.time;
        Shoot();

        stateMachine.ChangeState(EnemyStateType.Chase);
    }

    private void Shoot()
    {
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