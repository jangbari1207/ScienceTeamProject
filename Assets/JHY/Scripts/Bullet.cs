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
        {
            MagneticField playerField = collision.GetComponent<MagneticField>();
            MagneticInventory playerInventory = collision.GetComponent<MagneticInventory>();

            if (playerField != null && playerField.IsActive)
            {
                if (playerInventory != null)
                {
                    playerInventory.Add(this);
                }
                gameObject.SetActive(false);
                return;
            }
            else
            {
                collision.GetComponent<HealthModule>()?.TakeDamage(damage);

                gameObject.SetActive(false);
                return;
            }
        }

        if (collision.CompareTag("Enemy"))
        {
            HealthModule health = collision.GetComponent<HealthModule>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
            gameObject.SetActive(false);
            return;
        }
    }
}
