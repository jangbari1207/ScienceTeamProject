using System;
using UnityEngine;

public class HealthModule : MonoBehaviour
{
    [SerializeField] private int health = 100;

    public event Action OnDamaged;
    public event Action onDead;
    public bool IsDead => currentHealth <= 0;
    private int currentHealth;

    private void Awake()
    {
        currentHealth = health;
    }
    public void TakeDamage(int damage)
    {
        if (IsDead) return;
          currentHealth -= damage;
        Debug.Log(currentHealth);
          OnDamaged?.Invoke();
        
    }
    
    public void Dead()
    {
        Debug.Log("죽음");
    }

}
