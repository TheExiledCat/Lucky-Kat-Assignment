using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AM = null;
    AudioSource source;
    AudioClip bounceFX;
    private void Awake()
    {
        
            if (AM == null)
            {

                AM = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        source = GetComponent<AudioSource>();
        bounceFX = Resources.Load<AudioClip>("Sounds/Effects/Splat");
        
    }
    public void Bounce()
    {
        source.PlayOneShot(bounceFX);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
