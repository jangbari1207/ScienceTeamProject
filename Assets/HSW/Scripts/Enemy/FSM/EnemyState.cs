using UnityEngine;

public abstract class EnemyState
{
    protected AbstractEnemy enemy;
    protected EnemyStateMachine stateMachine;
    protected int animHash;
    protected bool aniEndTrigger;

    public EnemyState(AbstractEnemy enemy, EnemyStateMachine stateMachine,string aniBoolName)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        animHash = Animator.StringToHash(aniBoolName);
    }

    public virtual void Enter()
    {
        aniEndTrigger = false;
        enemy.AnimCompo.SetBool(animHash, true);
    }

    public virtual void Exit()
    {
        enemy.AnimCompo.SetBool(animHash, false);
    }

    public virtual void UpdateState()
    {
        
    }
    public virtual void AniEndTrigger()
    {
        aniEndTrigger = true;
    }
}
