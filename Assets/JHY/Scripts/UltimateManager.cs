using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UltimateManager : MonoBehaviour
{
    public static UltimateManager Instance { get; private set; }

    private float currentUltimateGage = 0f;
    private float maxUltimateGage = 100f;

    public bool CanUltimate { get; private set; } = false;
    [SerializeField] private Slider ultimateSlider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (ultimateSlider != null)
        {
            ultimateSlider.maxValue = maxUltimateGage;
            ultimateSlider.value = currentUltimateGage;
        }
    }

    private void Update()
    {
        if (CanUltimate && Keyboard.current.xKey.wasPressedThisFrame)
        {
            Ultimate();
        }
    }

    private void Ultimate()
    {
        QuizManager quizManager = GetComponent<QuizManager>();
        if (quizManager != null)
        {
            quizManager.StartQuiz();
        }
        else
        {
            Debug.LogError("같은 오브젝트에 QuizManager가 없습니다!");
        }

        currentUltimateGage = 0f;
        CanUltimate = false;

        if (ultimateSlider != null)
        {
            ultimateSlider.value = currentUltimateGage;
        }
    }
    public void PlusGage()
    {
        if (CanUltimate) return;

        currentUltimateGage += 5f;
        currentUltimateGage = Mathf.Clamp(currentUltimateGage, 0f, maxUltimateGage);

        if (ultimateSlider != null)
        {
            ultimateSlider.value = currentUltimateGage;
        }

        if (currentUltimateGage >= maxUltimateGage)
        {
            CanUltimate = true;
        }
    }
}
