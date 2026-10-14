using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private void Start()
    {
        if (WaveManager.Instance != null)
        {
            WaveManager.Instance.NextWave += HandleWave;
        }
    }

    private void OnDestroy()
    {
        if (WaveManager.Instance != null)
        {
            WaveManager.Instance.NextWave -= HandleWave;
        }
    }

    private void HandleWave(int waveCount)
    {
        Debug.Log($"현재 웨이브: {waveCount}");
    }
}
