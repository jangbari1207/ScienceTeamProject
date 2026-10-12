using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Polarity
{
    N,
    S
}
public class PlayerPolarity : MonoBehaviour
{
    public Polarity CurrentPolarity { get; private set; } = Polarity.N;

    [Header("Visual")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color nColor = Color.blue;
    [SerializeField] private Color sColor = Color.red;
    private void Awake()
    {
        UpdateColor();
    }
    private void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SwitchPolarity();
        }
    }
    private void SwitchPolarity()
    {
        CurrentPolarity =
            CurrentPolarity == Polarity.N
            ? Polarity.S
            : Polarity.N;

        UpdateColor();
    }
    private void UpdateColor()
    {
        if (spriteRenderer == null)
            return;

        spriteRenderer.color =
            CurrentPolarity == Polarity.N
            ? nColor
            : sColor;
    }

}
