using System;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }
    [SerializeField] private float maxNextTime = 30f;
    [SerializeField] private TextMeshProUGUI waveText;
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
    private void Start()
    {
        waveText.text = "Wave " + waveCount;
    }
    private void Update()
    {
        waveCurrentTime += Time.deltaTime;

        waveText.text = "Wave " + waveCount;
        if (waveCurrentTime >=  maxNextTime)
        {
            waveCount++;
            NextWave?.Invoke(waveCount);
            waveCurrentTime = 0f;
        }
    }
}
