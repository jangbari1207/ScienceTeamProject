using UnityEngine;

public class LaserDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            HealthModule health = collision.GetComponent<HealthModule>();
            if (health != null)
            {
                health.TakeDamage(10);
            }
        }
    }
}
