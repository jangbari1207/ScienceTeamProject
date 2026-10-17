using System.Collections.Generic;
using Unity.VisualScripting;

public enum EnemyStateType
{
    Chase,
    Attack,
    Hit,
    Dead
}

public class EnemyStateMachine
{
    private AbstractEnemy enemy;
    private Dictionary<EnemyStateType, EnemyState> stateDict = new();

    public EnemyState CurrentState { get; private set; }

    public void AddState(EnemyStateType type, EnemyState state)
    {
        stateDict.Add(type, state);
    }

    public void Initialize(EnemyStateType type,AbstractEnemy owner)
    {
        enemy = owner;
        CurrentState = stateDict[type];
        CurrentState.Enter();
    }

    public void ChangeState(EnemyStateType type)
    {
        if (CurrentState != null)
            CurrentState.Exit();

        CurrentState = stateDict[type];
        CurrentState.Enter();
    }
}