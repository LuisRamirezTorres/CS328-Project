using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;
    public Transform bullet_posiiton;
    private AudioManager audioManager;
    private GameObject player;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private float timer;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < 15)
        {
            timer += Time.deltaTime;

            if (timer > 2)
            {

                timer = 0;
                shoot();

            }
        }
    }
    void shoot()
    {
        audioManager.PlaySFX(audioManager.enemyGun);
        Instantiate(bullet, bullet_posiiton.position, Quaternion.identity);
    }
}
