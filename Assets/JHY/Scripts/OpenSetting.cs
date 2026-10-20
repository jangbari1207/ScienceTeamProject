using UnityEngine;
using UnityEngine.InputSystem;

public class OpenSetting : MonoBehaviour
{
    [SerializeField] private GameObject settingPanel;
    private bool isActive = false;
    private void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            isActive = !isActive;
           if(isActive)
            {
                settingPanel.SetActive(false);
            }
           else
            {
                settingPanel.SetActive(true);
            }
        }
    }
}
