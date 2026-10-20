using System.Collections;
using UnityEngine;

public class LaserDestroy : MonoBehaviour
{

    public float cooldown = 0.8f;
    
    void Start()
    {
        StartCoroutine(Destroy());
    }
    
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(cooldown);
        
        Destroy(gameObject);
    }
}
