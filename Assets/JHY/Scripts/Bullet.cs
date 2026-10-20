using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Bullet : MagneticObject
{
    [SerializeField] private BulletSO bulletSO;

    private SpriteRenderer spriteRenderer;
    private int damage;
    private bool isPlayerBullet = false; 

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

    public override void Fire(Vector2 direction)
    {
        base.Fire(direction);
        isPlayerBullet = true;
    }
    public override bool ApplyMagneticForce(Vector2 playerPosition, Polarity playerPolarity, Vector2 mouseDirection)
    {
        bool captured = base.ApplyMagneticForce(playerPosition, playerPolarity, mouseDirection);

        if (!captured && polarity == playerPolarity)
        {
            isPlayerBullet = true;
        }

        return captured;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
            return;
        }
        if (isPlayerBullet)
        {
            if (collision.CompareTag("Enemy"))
            {
                HealthModule health = collision.GetComponent<HealthModule>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                }
                gameObject.SetActive(false);
            }
            return;
        }

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
                PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }
                gameObject.SetActive(false);
                return;
            }
        }

        if (collision.CompareTag("Enemy"))
        {
            return;
        }
    }
}
