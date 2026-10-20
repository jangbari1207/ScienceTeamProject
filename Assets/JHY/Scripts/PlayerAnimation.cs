using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private readonly int _hashSpeed = Animator.StringToHash("Speed");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponentInParent<SpriteRenderer>();
        if (_spriteRenderer == null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    public void SetAnimation(PlayerMovement playerMovement)
    {
        if (PlayerHealth.Instance.IsDead) return;
        float currentSpeed = Mathf.Abs(playerMovement.RbCompo.linearVelocityX);
        _animator.SetFloat(_hashSpeed, currentSpeed);

        if (playerMovement.RbCompo.linearVelocityX > 0.01f)
        {
            if (_spriteRenderer != null) _spriteRenderer.flipX = false;
        }
        else if (playerMovement.RbCompo.linearVelocityX < -0.01f)
        {
            if (_spriteRenderer != null) _spriteRenderer.flipX = true;
        }
    }
}
