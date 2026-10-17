using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private AbstractEnemy enemy;
    private void Awake()
    {
        enemy = GetComponentInParent<AbstractEnemy>();
    }
    public void AnimationEnd()
    {
        enemy.AnimationEndTrigger();
    }
    public void AnimationFire()
    {
        enemy.AnimationFireTrigger();
    }
}
