using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clips")] 
    public AudioClip miguelGun;
    public AudioClip miguelPunch;
    public AudioClip enemyGun;
    public AudioClip obtainStim;
    public AudioClip useStim;
    public AudioClip music;
    
    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = music;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip audioClip)
    {
        SFXSource.clip = audioClip;
        SFXSource.PlayOneShot(audioClip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
