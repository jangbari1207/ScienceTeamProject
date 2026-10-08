using System;
using UnityEngine;

public class HealthModule : MonoBehaviour
{
    [SerializeField] private int health = 100;

    public event Action OnDamaged;
    public event Action onDead;

    private int currentHealth;

    private void Awake()
    {
        currentHealth = health;
    }
    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;
          currentHealth -= damage;
          OnDamaged?.Invoke();
        if(currentHealth <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        Debug.Log("죽음");
    }

}
