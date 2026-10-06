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

    [SerializeField]private float parryWindow = 0.4f;
    private float lastSwitchTime = -999f;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color nColor = Color.blue;
    [SerializeField] private Color sColor = Color.red;

    private void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SwitchPolarity();
        }
    }

    private void SwitchPolarity()
    {
        CurrentPolarity = (CurrentPolarity == Polarity.N) ? Polarity.S : Polarity.N;
        lastSwitchTime = Time.time;

        if(spriteRenderer != null)
        {
            spriteRenderer.color = (CurrentPolarity == Polarity.N) ? nColor : sColor;
        }
    }
    public bool IsParryTiming()
    {
        return Time.time - lastSwitchTime <= parryWindow;
    }

}
