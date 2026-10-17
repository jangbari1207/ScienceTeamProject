using System.Collections;
using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    [Header("적 프리팹 (순서대로 1, 2, 3번 적)")]
    [SerializeField] private GameObject[] enemyPrefabs; 
    [SerializeField] private GameObject bossPrefab;      

    [Header("스폰 위치")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnRadius = 12f;

    private Coroutine spawnCoroutine;

    private void Start()
    {
        if (WaveManager.Instance != null)
        {
            WaveManager.Instance.NextWave += HandleWave;
            StartWaveSpawning(WaveManager.Instance.waveCount);
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
        StartWaveSpawning(waveCount);
    }

    private void StartWaveSpawning(int wave)
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
        spawnCoroutine = StartCoroutine(SpawnRoutine(wave));
    }

    private IEnumerator SpawnRoutine(int wave)
    {
        float spawnInterval = Mathf.Max(0.6f, 2.0f - (wave * 0.15f));

        while (true)
        {
            SpawnEnemy(wave);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy(int wave)
    {
        Vector3 spawnPos = GetRandomSpawnPosition();
        GameObject targetPrefab = null;

        if (wave == 10)
        {
            targetPrefab = bossPrefab;
        }
        else
        {
            int maxIndex = 0;

            if (wave < 3)
            {
                maxIndex = 0; 
            }
            else if (wave < 6)
            {
                maxIndex = 1;
            }
            else
            {
                maxIndex = 2; 
            }

            int randomIndex = Random.Range(0, maxIndex + 1);
            targetPrefab = enemyPrefabs[randomIndex];
        }

        if (targetPrefab != null)
        {
            Instantiate(targetPrefab, spawnPos, Quaternion.identity);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        if (spawnPoints != null && spawnPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            return spawnPoints[randomIndex].position;
        }

        Vector2 randomDir = Random.insideUnitCircle.normalized;
        return (Vector2)transform.position + randomDir * spawnRadius;
    }
}
