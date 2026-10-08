using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MagneticField : MonoBehaviour
{
    [Header("Field")]
    [SerializeField] private float radius = 5f;
    [SerializeField] private float duration = 5f;

    public bool IsActive { get; private set; }
    private float currentTime;
    private PlayerPolarity playerPolarity;
    private MagneticInventory inventory;
    private void Awake()
    {
        playerPolarity = GetComponent<PlayerPolarity>();
        inventory = GetComponent<MagneticInventory>();
    }
    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            ToggleField();
        }

        if (!IsActive)
            return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            IsActive = false;
        }
    }
    private void FixedUpdate()
    {
        if (!IsActive)
            return;

        Vector2 mousePosition = Vector2.zero;
        if (Mouse.current != null)
        {
            mousePosition = Mouse.current.position.ReadValue();
        }
        Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 mouseDirection = (worldMousePosition - (Vector2)transform.position).normalized;

        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D collider in objects)
        {
            MagneticObject magneticObject = collider.GetComponent<MagneticObject>();

            if (magneticObject == null)
                continue;

            bool captured = magneticObject.ApplyMagneticForce(
                transform.position,
                playerPolarity.CurrentPolarity,
                mouseDirection
            );

            if (captured)
            {
                inventory.Add(magneticObject);
            }
        }
    }
    private void ToggleField()
    {
        IsActive = !IsActive;

        if (IsActive)
        {
            currentTime = duration;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
