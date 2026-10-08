using UnityEngine;

[CreateAssetMenu(fileName = "BulletSO", menuName = "SO/BulletSO")]
public class BulletSO : ScriptableObject
{
    public float speed = 8f;
    public int damage = 5;
    public Color color = Color.white;
    public Polarity polarity;
}
