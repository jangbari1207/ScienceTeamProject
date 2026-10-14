using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(EnemyAI enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void UpdateState()
    {
        Vector2 dir =
            enemy.Player.position - enemy.transform.position;
        
        float distance =  dir.magnitude;

        if (distance <= enemy.attackRange)
        {
            stateMachine.ChangeState(EnemyStateType.Attack);
            return;
        }
        
        enemy.transform.position += (Vector3)(dir.normalized * enemy.moveSpeed * Time.deltaTime);
    }
    
}
