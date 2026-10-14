using System.Collections.Generic;

public enum EnemyStateType
{
    Chase,
    Attack
}

public class EnemyStateMachine
{
    private Dictionary<EnemyStateType, EnemyState> stateDict = new();

    public EnemyState CurrentState { get; private set; }

    public void AddState(EnemyStateType type, EnemyState state)
    {
        stateDict.Add(type, state);
    }

    public void Initialize(EnemyStateType type)
    {
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