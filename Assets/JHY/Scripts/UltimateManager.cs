using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class UltimateManager : MonoBehaviour
{
    private float currentUltimateGage=0f;
    private float maxUltimateGage = 100f;

    private HealthModule health;

    public bool CanUltimate { get; private set; } = false;
    private void Awake()
    {
        health = GetComponent<HealthModule>();
    }
    private void Start()
    {
        health.onDead += PlusGage;
    }
    private void OnDestroy()
    {
        health.onDead -= PlusGage;
    }
    private void Update()
    {
        if (!CanUltimate) return;

        if(CanUltimate&&Keyboard.current.xKey.wasPressedThisFrame)
        {
            Ultimate();
        }
    }

    private void Ultimate()
    {

    }

    private void PlusGage()
    {
        currentUltimateGage += 5f;
        if (currentUltimateGage >= maxUltimateGage)
        {
            CanUltimate = true;
        }
    }
}
