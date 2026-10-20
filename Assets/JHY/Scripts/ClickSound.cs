using UnityEngine;

public class ClickSound : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound;

    public void Click()
    {
        SoundManager.Instance.PlaySFX(clickSound);
    }
}
