using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(
        EnemyAI enemy,
        EnemyStateMachine stateMachine)
        : base(enemy, stateMachine)
    {
    }

    public override void UpdateState()
    {
        float distance = Vector2.Distance(
            enemy.transform.position,
            enemy.Player.position);

        if (distance > enemy.attackRange)
        {
            stateMachine.ChangeState(EnemyStateType.Chase);
            return;
        }

        if (Time.time >= enemy.lastShotTime + enemy.fireRate)
        {
            Shoot();
            enemy.lastShotTime = Time.time;
        }
    }

    private void Shoot()
    {
        Vector2 dir =
            (enemy.Player.position - enemy.firePoint.position).normalized;

        GameObject bullet = Object.Instantiate(
            enemy.bulletPrefab,
            enemy.firePoint.position,
            Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = dir * 8f;
    }
}