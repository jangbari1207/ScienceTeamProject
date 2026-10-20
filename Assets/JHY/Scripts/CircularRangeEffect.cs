using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CircularRangeEffect : MonoBehaviour
{
    [Header("설정")]
    [SerializeField] private float scaleStep = 1.5f;
    [SerializeField] private float baseRadius = 2.0f;
    [Header("데미지 설정")]
    [SerializeField] private int baseDamage = 10;                     
    [SerializeField] private float damageMultiplierStep = 1.5f;
    [SerializeField] private int ultimateDamage = 10;
    [SerializeField] private float animationDuration = 1.0f;
    private CinemachineImpulseSource impulseSource;
    private Animator ani;
    private Vector3 initialScale;
    private int currentComboCount;
    private void Awake()
    {
        ani = GetComponent<Animator>();
        initialScale = transform.localScale;
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void ShowEffect(int comboCount)
    {
        currentComboCount = comboCount;
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
            impulseSource.GenerateImpulse();
            ani.SetTrigger("Boom");
        }

        yield return new WaitForSeconds(animationDuration);

        gameObject.SetActive(false);
    }
    private void ApplyDamageToEnemies()
    {
        float currentRadius = baseRadius * transform.localScale.x;
        Vector2 centerPos = transform.position;
        int finalDamage = Mathf.RoundToInt(baseDamage * (1f + currentComboCount * damageMultiplierStep));
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(centerPos, currentRadius);

        foreach (Collider2D hit in hitColliders)
        {
            if (hit.CompareTag("Enemy"))
            {
                var enemyScript = hit.GetComponent<HealthModule>();
                if (enemyScript != null)
                {
                    enemyScript.TakeDamage(finalDamage);
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
