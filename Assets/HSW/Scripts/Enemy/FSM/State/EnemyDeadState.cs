using UnityEngine;

public class EnemyDeadState : EnemyState
{
    public EnemyDeadState(AbstractEnemy enemy, EnemyStateMachine stateMachine, string aniBoolName) : base(enemy, stateMachine, aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SoundManager.Instance.PlaySFX(enemy.deadSound);
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if(aniEndTrigger)
        {
            Object.Destroy(enemy.gameObject);
        }
    }
}
