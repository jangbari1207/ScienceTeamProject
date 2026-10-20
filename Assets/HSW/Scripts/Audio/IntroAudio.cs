using System;
using System.Collections;
using UnityEngine;

public class IntroAudio : MonoBehaviour
{
    public AudioClip intro;
    public AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();

        if (source == null)
        {
            source = gameObject.AddComponent<AudioSource>();
        }
    }

    void Start()
    {
        source.clip = intro;

        StartCoroutine(MusicStart());
    }

    IEnumerator MusicStart()
    {
        Debug.Log("started");
        
        yield return new WaitForSeconds(1f);
        source.volume = 0.5f;
        
        source.Play();
    }
    
    

}
