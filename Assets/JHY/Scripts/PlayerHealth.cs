using DG.Tweening;
using System;
using System.Collections;
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
    [SerializeField] private GameObject waveText;
    [SerializeField] private GameObject ultimateGage;

    [Header("피격 연출 설정")]
    [SerializeField] private float flashDuration = 0.1f; 

    public event Action OnDamaged;
    public event Action OnDead;
    public bool IsDead => currentHealth <= 0;
    public bool IsInvincible { get; set; } = false;
    private int currentHealth;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public AudioClip hitSound;
    public AudioClip deadSound;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        currentHealth = maxHealth;
        UpdateHealthUI();

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }

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
        SoundManager.Instance.PlaySFX(hitSound);
        OnDamaged?.Invoke();
        UpdateHealthUI();

        if (spriteRenderer != null)
        {
            StartCoroutine(FlashRoutine());
        }

        if (IsDead)
        {
            Die();
        }
    }

    private IEnumerator FlashRoutine()
    {
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }
    public void Heal(int amount)
    {
        if (IsDead) return;

        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        UpdateHealthUI();
    }
    private void Die()
    {
        SoundManager.Instance.PlaySFX(deadSound);
        OnDead?.Invoke();
        if (healthUIObject != null)
        {
            healthUIObject.SetActive(false);
        }
        if (ultimateUI != null)
        {
            ultimateUI.SetActive(false);
        }
        if (waveText != null)
        {
            waveText.SetActive(false);
        }
        if(ultimateGage != null)
        {
            ultimateGage.SetActive(false);
        }
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            CanvasGroup canvasGroup = gameOverPanel.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0f;
                canvasGroup.DOFade(1f, 1f).SetUpdate(true);
            }
        }
        gameObject.SetActive(false);
    }
    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }
    public void GoMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
