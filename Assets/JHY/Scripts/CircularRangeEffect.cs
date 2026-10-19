using System.Collections;
using UnityEngine;

public class CircularRangeEffect : MonoBehaviour
{
    [Header("설정")]
    [SerializeField] private float scaleStep = 1.5f;
    [SerializeField] private float baseRadius = 2.0f;
    [SerializeField] private int ultimateDamage = 50;
    [SerializeField] private float animationDuration = 1.0f;

    private Animator ani;
    private Vector3 initialScale;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        initialScale = transform.localScale;
    }

    public void ShowEffect(int comboCount)
    {
        gameObject.SetActive(true);
        UpdateScale(comboCount);
    }

    public void UpdateScale(int comboCount)
    {
        float currentMultiplier = 1f + (comboCount * scaleStep);
        transform.localScale = initialScale * currentMultiplier;
    }

    public void PlayBoomAndDamage(bool isSuccess)
    {
        StartCoroutine(BoomRoutine(isSuccess));
    }

    private IEnumerator BoomRoutine(bool isSuccess)
    {
        if (isSuccess)
        {
            ApplyDamageToEnemies();
        }

        if (ani != null)
        {
            ani.SetTrigger("Boom");
        }

        yield return new WaitForSeconds(animationDuration);

        gameObject.SetActive(false);
    }
    private void ApplyDamageToEnemies()
    {
        float currentRadius = baseRadius * transform.localScale.x;
        Vector2 centerPos = transform.position;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(centerPos, currentRadius);

        foreach (Collider2D hit in hitColliders)
        {
            if (hit.CompareTag("Enemy"))
            {
                var enemyScript = hit.GetComponent<HealthModule>();
                if (enemyScript != null)
                {
                    enemyScript.TakeDamage(ultimateDamage);
                }

                Debug.Log($"적중한 적: {hit.name}");
            }
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.purple;
        Gizmos.DrawWireSphere(transform.position, baseRadius);
    }
}
