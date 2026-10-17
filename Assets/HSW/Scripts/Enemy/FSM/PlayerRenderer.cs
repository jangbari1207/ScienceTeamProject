using UnityEngine;

public class PlayerRenderer : MonoBehaviour
{
    private Transform _parentTrm;

    private void Awake()
    {
        _parentTrm = transform.parent;
    }

    public void Flipx(float x)
    {
        if (x < 0)
            _parentTrm.eulerAngles = new Vector3(0, 180, 0);
        else if (x > 0)
            _parentTrm.eulerAngles = new Vector3(0, 0, 0);
    }
}
