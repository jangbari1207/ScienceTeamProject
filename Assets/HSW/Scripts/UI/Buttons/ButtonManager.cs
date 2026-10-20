using System;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject SettingPanel;

    public void Play()
    {
        SceneManager.LoadScene("JHY/Scenes/SampleScene");
    }

    public void Exit()
    {
        
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
