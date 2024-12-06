using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    //public PowerUpEffect powerUpEffect;
    public float decreaseAmount;
    public float damageIncrease;
    public CyberPsychosisHandler psychosis;
    public GameObject miguel;

    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioManager.PlaySFX(audioManager.obtainStim);
        Destroy(gameObject);
        psychosis.psychosisLevel -= decreaseAmount;
        miguel.GetComponent<PlayerMovement>().damage += damageIncrease;
        //powerUpEffect.Apply(collision.gameObject);
    }
}
