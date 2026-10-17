using UnityEngine;

public class MagneticObject : MonoBehaviour
{
    protected Polarity polarity;
    public Polarity Polarity => polarity; 

    [Header("Magnetic")]
    [SerializeField] private float magneticForce = 10f;
    [SerializeField] private float captureDistance = 0.7f;

    protected float fireSpeed = 15f;
    private Rigidbody2D rb;
    private PlayerPolarity player;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindFirstObjectByType<PlayerPolarity>();
    }

    public virtual bool ApplyMagneticForce(Vector2 playerPosition, Polarity playerPolarity, Vector2 mouseDirection)
    {
        Vector2 directionToPlayer = playerPosition - rb.position;
        float distance = directionToPlayer.magnitude;

        if (polarity != playerPolarity)
        {
            if (distance <= captureDistance)
            {
                Capture();
                return true; 
            }

            rb.linearVelocity = directionToPlayer.normalized * magneticForce;
        }
        else
        {
            rb.linearVelocity = mouseDirection * fireSpeed;
        }

        return false;
    }

    private void Capture()
    {
        rb.linearVelocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    public virtual void Fire(Vector2 direction)
    {
        gameObject.SetActive(true);

        if (player != null)
        {
            transform.position = player.transform.position + (Vector3)(direction.normalized * 1.2f);
        }

        rb.linearVelocity = direction.normalized * fireSpeed;
    }
    public virtual void Drop()
    {
        gameObject.SetActive(true);

        if (player != null)
        {
            transform.position = player.transform.position + (Vector3)(Random.insideUnitCircle * 0.5f);
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; // 속도 초기화
        }
    }
}
