using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    protected EnemyAi enemy;
    protected EnemyStateMachine stateMachine;

    public EnemyState(EnemyAi enemy, EnemyStateMachine stateMachine)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        
    }

    public virtual void Exit()
    {
        
    }

    public virtual void UpdateState()
    {
        
    }
}
