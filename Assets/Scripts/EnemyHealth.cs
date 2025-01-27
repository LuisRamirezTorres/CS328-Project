using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public float health;
    public float currentHealth;

    public Collider2D enemyCollider;

    private Animator anim; 

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = health;
    }

    // Update is called once per frame
    /*void Update()
    {

        if (health < currentHealth)
        {
            currentHealth = health;
            anim.SetTrigger("Attacked");
        }


        if (health <= 0)
        {
            anim.SetBool("isDead", true);
            Debug.Log("Enemy is dead");

            enemyCollider.enabled = false;
        }
    }*/

    public void TakeDamage(float damage)
    {
        health -= damage;
        
        if (health < currentHealth)
        {
            currentHealth = health;
            anim.SetTrigger("Attacked");
        }


        if (health <= 0)
        {
            anim.SetBool("isDead", true);
            Debug.Log("Enemy is dead");

            enemyCollider.enabled = false;
        }
    }
}
