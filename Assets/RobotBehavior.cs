using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;
    public Transform bullet_posiiton;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private float timer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2)
        {
            timer = 0;
            shoot();
        }
    }
    void shoot()
    {
        audioManager.PlaySFX(audioManager.enemyGun);
        Instantiate(bullet, bullet_posiiton.position, Quaternion.identity);
    }
}
