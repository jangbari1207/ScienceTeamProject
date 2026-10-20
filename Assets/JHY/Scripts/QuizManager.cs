using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class QuizManager : MonoBehaviour
{

    [Header("문제 패널들 (순서대로 넣기)")]
    [SerializeField] private GameObject[] quizPanels;

    [Header("원형 범위 연출 컴포넌트 연결")]
    [SerializeField] private CircularRangeEffect rangeEffect;

    private int currentPanelIndex = 0;
    private int comboCount = 0;

    public void StartQuiz()
    {
        currentPanelIndex = 0;
        comboCount = 0;
        if (PlayerHealth.Instance != null)
        {
            PlayerHealth.Instance.IsInvincible = true;
        }
        if (rangeEffect != null)
        {
            rangeEffect.ShowEffect(comboCount);
        }
        else
        {
            Debug.LogWarning("CircularRangeEffect가 연결되지 않았습니다!");
        }

        for (int i = 0; i < quizPanels.Length; i++)
        {
            if (quizPanels[i] != null)
                quizPanels[i].SetActive(i == 0);
        }
    }

    public void OnCorrectAnswer()
    {
        comboCount++;

        if (rangeEffect != null)
        {
            rangeEffect.UpdateScale(comboCount);
        }

        quizPanels[currentPanelIndex].SetActive(false);
        currentPanelIndex++;

        if (currentPanelIndex < quizPanels.Length)
        {
            quizPanels[currentPanelIndex].SetActive(true);
        }
        else
        {
            SuccessUltimate();
        }
    }

    public void OnWrongAnswer()
    {
        Debug.Log("틀렸습니다! 궁극기 실패");
        EndQuiz(false); // 실패 시 데미지 X
    }
    private void SuccessUltimate()
    {
        Debug.Log("모든 문제 정답! 강력한 궁극기 발동");
        EndQuiz(true); // 성공 시 데미지 O
    }
    private void EndQuiz(bool isSuccess)
    {
        if (PlayerHealth.Instance != null)
        {
            PlayerHealth.Instance.IsInvincible = false;
        }
        // 퀴즈 패널들은 즉시 끔
        for (int i = 0; i < quizPanels.Length; i++)
        {
            if (quizPanels[i] != null)
                quizPanels[i].SetActive(false);
        }

        // 원형 이펙트 연출 및 비활성화 위임
        if (rangeEffect != null)
        {
            rangeEffect.PlayBoomAndDamage(isSuccess);
        }
    }
    
}
