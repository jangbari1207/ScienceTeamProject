using UnityEngine;

public class EnemyHitState : EnemyState
{
    public EnemyHitState(AbstractEnemy enemy, EnemyStateMachine stateMachine, string aniBoolName) : base(enemy, stateMachine, aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(aniEndTrigger)
        {
            stateMachine.ChangeState(EnemyStateType.Chase);
        }
    }
}
