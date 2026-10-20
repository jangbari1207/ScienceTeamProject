using UnityEngine;

public class EnemyHitState : EnemyState
{
    public EnemyHitState(AbstractEnemy enemy, EnemyStateMachine stateMachine, string aniBoolName) : base(enemy, stateMachine, aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SoundManager.Instance.PlaySFX(enemy.hitSound);
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
