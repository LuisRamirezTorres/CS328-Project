using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrowing : MonoBehaviour
{
    public GameObject knife;
    public Transform knifePosition; 

    private float timer;
    private GameObject player;

    EnemyHealth health;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        health = GetComponent<EnemyHealth>();   
    }

    // Update is called once per frame
    void Update()
    {
        

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < 7)
        {
            timer += Time.deltaTime;
            if (timer > 3f && health.health != 0)
            {
                anim.SetBool("isThrowing", true);
                timer = 0;
                throwKnife();

            }


        }

        if (health.currentHealth <=0 || health.health <= 0)
        {
            anim.SetBool("isDead", true);
            return;
        }

    }

    public void throwKnife()
    {
        Instantiate(knife, knifePosition.position, Quaternion.identity);
    }

    public void endThrowing()
    {
        anim.SetBool("isThrowing", false);
    }

}
