using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance { get; private set; }
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject healthUIObject;
    [SerializeField] private GameObject ultimateUI;
    public event Action OnDamaged;
    public event Action OnDead;
    public bool IsDead => currentHealth <= 0;
    public bool IsInvincible { get; set; } = false;
    private int currentHealth;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        currentHealth = maxHealth;
        UpdateHealthUI();

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        if (IsDead || IsInvincible) return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);

        Debug.Log($"플레이어 남은 체력: {currentHealth}");
        OnDamaged?.Invoke();
        UpdateHealthUI();

        if (IsDead)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("플레이어 사망");
        OnDead?.Invoke();
        if (healthUIObject != null)
        {
            healthUIObject.SetActive(false);
        }
        if(ultimateUI != null)
        {
            ultimateUI.SetActive(false);
        }
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
