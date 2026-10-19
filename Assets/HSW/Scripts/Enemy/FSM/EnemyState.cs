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

        if (enemy.AnimCompo != null &&
            enemy.AnimCompo.runtimeAnimatorController != null)
        {
            enemy.AnimCompo.SetBool(animHash, true);
        }
    }

    public virtual void Exit()
    {
        if (enemy.AnimCompo != null &&
            enemy.AnimCompo.runtimeAnimatorController != null)
        {
            enemy.AnimCompo.SetBool(animHash, false);
        }
    }

    public virtual void UpdateState()
    {
        
    }
    public virtual void AniFireTrigger() { }
    public virtual void AniEndTrigger()
    {
        aniEndTrigger = true;
    }
}
