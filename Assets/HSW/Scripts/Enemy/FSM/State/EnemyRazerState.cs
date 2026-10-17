using UnityEngine;

public class EnemyRazerState : EnemyState
{
    public EnemyRazerState(AbstractEnemy enemy, EnemyStateMachine stateMachine, string aniBoolName) : base(enemy, stateMachine, aniBoolName)
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
}
