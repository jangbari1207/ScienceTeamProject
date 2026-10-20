using System;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject SettingPanel;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip wrongSound;
   
    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Exit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    public void ButtonClick()
    {
        SoundManager.Instance.PlaySFX(clickSound);
    }
    public void CorrectButton()
    {
        SoundManager.Instance.PlaySFX(correctSound);
    }
    public void WrongButton()
    {
        SoundManager.Instance.PlaySFX(wrongSound);
    }
    
    public void Setting()
    {
        if (SettingPanel != null)
        {
            SettingPanel.SetActive(true);
        }
    }

    public void SettingClose()
    {
        if (SettingPanel != null)
        {
            SettingPanel.SetActive(false);
        }
    }
    
    
}
