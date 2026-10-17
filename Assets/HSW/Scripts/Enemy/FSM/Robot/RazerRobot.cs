using UnityEngine;

public class RazerRobot : AbstractEnemy
{
    public EnemyStateMachine stateMachine = new EnemyStateMachine();
    private HealthModule health;
    protected override void Awake()
    {
        base.Awake();
        health = GetComponent<HealthModule>();
        stateMachine.AddState(EnemyStateType.Idle, new EnemyIdleState(this, stateMachine, "Idle"));
        stateMachine.AddState(EnemyStateType.Chase, new EnemyChaseState(this, stateMachine, "Chase"));
        stateMachine.AddState(EnemyStateType.Attack, new EnemyRazerState(this, stateMachine, "Attack"));
        stateMachine.AddState(EnemyStateType.Hit, new EnemyHitState(this, stateMachine, "Hit"));
        stateMachine.AddState(EnemyStateType.Dead, new EnemyDeadState(this, stateMachine, "Dead"));

        stateMachine.Initialize(EnemyStateType.Chase, this);
    }
    private void Start()
    {
        health.OnDamaged += () => stateMachine.ChangeState(EnemyStateType.Hit);
        health.onDead += () => stateMachine.ChangeState(EnemyStateType.Dead);
    }
    private void Update()
    {
        stateMachine.CurrentState.UpdateState();
    }
    public override void AnimationEndTrigger()
    {
        base.AnimationEndTrigger();
        stateMachine.CurrentState.AniEndTrigger();
    }
    public override void AnimationFireTrigger()
    {
        base.AnimationFireTrigger();
        stateMachine.CurrentState.AniFireTrigger();
    }
}
