using System.Collections;
using UnityEngine;

public class LaserDestroy : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Destroy());
    }
    
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1f);
        
        Destroy(gameObject);
    }
}
