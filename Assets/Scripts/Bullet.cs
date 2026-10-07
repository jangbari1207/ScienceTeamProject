using NUnit.Framework.Internal;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public enum BulletState
{
    Fire,
    Reflect
}
public class Bullet : MonoBehaviour
{
    [SerializeField]private PlayerPolarity playerPolarity;

    //[SerializeField]private HealthModule health;
    [Header("Bullet Settings")]

    private float speed;
    private BulletSO bulletSO;
    public Polarity bulletPolarity;
    public BulletState bulletState;
    private SpriteRenderer sr;
    private Vector2 moveDir;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    public void Init(Vector2 direction , BulletSO data)
    {
        moveDir = direction;
        speed = data.speed;
        sr.color = data.color;
        bulletPolarity = data.polarity;
        bulletState = data.bulletState;
    }

    public void Update()
    {
        transform.Translate(moveDir * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bulletState == BulletState.Fire && collision.CompareTag("Player"))
        {
            HealthModule playerHealth = collision.GetComponent<HealthModule>();
            PlayerPolarity player = collision.GetComponent<PlayerPolarity>();
            if(player == null) return;

            if(player.CurrentPolarity == bulletPolarity)
            {
                if(player.IsParryTiming())
                {
                    //ReflectToEnemy();
                }
                else
                {
                    //health.TakeDamage(5);
                }
            }
            else
            {
                //ReflectToEnemy();
            }
        }
        else if(bulletState == BulletState.Reflect && collision.CompareTag("Enemy"))
        {
            //HealthModule enemyHealth = collision.GetComponent<HealthModule>();
            //if(enemyHealth != null)
            //{
            //    enemyHealth.TakeDamage(5);
            //}
        }
    }
}
