using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private readonly int _hashX = Animator.StringToHash("VelocityX");
    private readonly int _hashY = Animator.StringToHash("VelocityY");

    public void SetAnimation(PlayerMovement playerMovement)
    {

        _animator.SetFloat(_hashX, playerMovement.RbCompo.linearVelocityX);
        _animator.SetFloat(_hashY, playerMovement.RbCompo.linearVelocityY);
    }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

}
