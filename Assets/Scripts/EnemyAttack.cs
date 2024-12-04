using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{


    public Animator anim;
    public int enemyDamage;
    public float radius;
    public float distanceBetween;

    private float distance;

    public GameObject player;
    public HealthHandler playerHealthHandler;
    public GameObject punchPoint;
    public GameObject runPunchPoint; 
    

    public Rigidbody2D rb;

    public LayerMask players;
    // Start is called before the first frame update
    void Start()
    {
        anim.SetFloat("Speed", rb.velocity.x);
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate distance between the enemy and the player
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < distanceBetween)
        {
            if (distance < .7f)
            {
                // Stop enemy movement and punch the player
                rb.velocity = Vector2.zero; // Stops the enemy in place
                anim.SetFloat("Speed", 0f); // Stop movement animation
                anim.SetBool("isPunching", true); // Start punching animation
            }
            else
            {
                // Enemy keeps moving toward the player but is ready to punch
                anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x)); // Continue movement animation
                anim.SetBool("isPunching", false); // Ensure punching animation is off
            }
        }
        else
        {
            // Enemy is out of range, reset animations
            anim.SetBool("isPunching", false);
        }
    }

    public void punch()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(punchPoint.transform.position, radius, players);

        foreach (Collider2D playerGameObject in player)
        {
            Debug.Log("Player Hit");
            //playerGameObject.GetComponent<HealthHandler>().health -= enemyDamage;
            playerHealthHandler.health -= enemyDamage;
        }
    }

    public void runPunch()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(runPunchPoint.transform.position, radius, players);

        foreach (Collider2D playerGameObject in player)
        {
            Debug.Log("Player Hit");
            //playerGameObject.GetComponent<HealthHandler>().health -= enemyDamage;
            playerHealthHandler.health -= enemyDamage;
        }
    }

    public void endPunch()
    {
        anim.SetBool("isPunching", false);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(punchPoint.transform.position, radius);
        Gizmos.DrawWireSphere(runPunchPoint.transform.position, radius);

    }

}
