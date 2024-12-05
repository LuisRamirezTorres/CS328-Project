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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        psychosis.psychosisLevel -= decreaseAmount;
        miguel.GetComponent<PlayerMovement>().damage += damageIncrease;
        //powerUpEffect.Apply(collision.gameObject);
    }
}
