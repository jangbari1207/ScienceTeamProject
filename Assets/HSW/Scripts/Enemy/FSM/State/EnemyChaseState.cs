using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(AbstractEnemy enemy, EnemyStateMachine stateMachine, string aniBoolName) : base(enemy, stateMachine, aniBoolName)
    {
    }

    public override void UpdateState()
    {
        Vector2 dir =
            enemy.Player.position - enemy.transform.position;
        enemy.RenderCompo.Flipx(dir.x);
        float distance =  dir.magnitude;

        if (distance > enemy.attackRange)
        {
            enemy.transform.position += (Vector3)(dir.normalized * enemy.moveSpeed * Time.deltaTime);
            return;
        }

        if (Time.time < enemy.lastShotTime + enemy.fireRate)
        {
            return;
        }

        stateMachine.ChangeState(EnemyStateType.Attack);
    }
    
}
