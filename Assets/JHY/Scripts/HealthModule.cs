using System;
using UnityEngine;

public class HealthModule : MonoBehaviour
{
    [SerializeField] private int health = 100;

    public event Action OnDamaged;
    public event Action onDead;
    private Collider2D col;
    public bool IsDead => currentHealth <= 0;
    private int currentHealth;

    private void Awake()
    {
        currentHealth = health;
        col = GetComponent<Collider2D>();
    }
    public void TakeDamage(int damage)
    {
        if (IsDead) return;

        currentHealth -= damage;
        Debug.Log($"남은 체력: {currentHealth}");
        OnDamaged?.Invoke();

        if (IsDead)
        {
            Dead();
        }

    }
    
    public void Dead()
    {
        Debug.Log("죽음");
        if (col != null)
        {
            col.enabled = false;
        }
        onDead?.Invoke();
        if (UltimateManager.Instance != null)
        {
            UltimateManager.Instance.PlusGage();
        }
    }

}
