using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }
    [SerializeField] private float maxNextTime = 30f;
    private float waveCurrentTime;
    public int waveCount { get; private set; } = 1;

    public event Action<int> NextWave;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        waveCurrentTime += Time.deltaTime;

        if (waveCurrentTime >=  maxNextTime)
        {
            waveCount++;
            NextWave?.Invoke(waveCount);
            waveCurrentTime = 0f;
        }
    }
}
