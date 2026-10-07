using UnityEngine;

[CreateAssetMenu(fileName = "BulletSO", menuName = "SO/BulletSO")]
public class BulletSO : ScriptableObject
{
    public float speed;
    public Color color;

    public BulletState bulletState;

    public Polarity polarity;
}
