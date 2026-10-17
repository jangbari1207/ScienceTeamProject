using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(AbstractEnemy enemy, EnemyStateMachine stateMachine, string aniBoolName) : base(enemy, stateMachine, aniBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();

        Vector2 dir = enemy.Player.position - enemy.transform.position;
        enemy.RenderCompo.Flipx(dir.x);

        if (dir.magnitude > enemy.attackRange)
        {
            stateMachine.ChangeState(EnemyStateType.Chase);
            return;
        }

        if (Time.time < enemy.lastShotTime + enemy.fireRate)
        {
            return;
        }

        stateMachine.ChangeState(EnemyStateType.Attack);
    }
}
