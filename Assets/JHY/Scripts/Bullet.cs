using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Bullet : MagneticObject
{
    [SerializeField] private BulletSO bulletSO;

    private SpriteRenderer spriteRenderer;
    private int damage;

    protected override void Awake()
    {
        base.Awake();

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (bulletSO != null)
        {
            polarity = bulletSO.polarity;
            fireSpeed = bulletSO.speed;
            damage = bulletSO.damage;
            spriteRenderer.color = bulletSO.color;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            return;

        if (collision.CompareTag("Enemy"))
        {
            HealthModule health = collision.GetComponent<HealthModule>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }

        gameObject.SetActive(false);
    }
}
