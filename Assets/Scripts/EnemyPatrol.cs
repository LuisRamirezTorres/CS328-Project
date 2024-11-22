using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    public GameObject player;
    public GameObject pointA;
    public GameObject pointB;
    public float speed;
    public float distanceBetween;

    private EnemyHealth enemyHealth;

    private float distance;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    


    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        anim.SetFloat("Speed", speed);

        enemyHealth = GetComponent<EnemyHealth>();

    }

    // Update is called once per frame
    void Update()
    {

        if (enemyHealth.health <= 0f)
        {
            rb.velocity = Vector2.zero;
            anim.SetFloat("Speed", 0f);
            return;
        }


        Vector2 point = currentPoint.position - transform.position;
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < distanceBetween)
        {
            // Chase the player
            Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
            rb.velocity = new Vector2(directionToPlayer.x * speed, rb.velocity.y);

            // Ensure facing the correct direction
            if ((directionToPlayer.x > 0 && transform.localScale.x < 0) ||
                (directionToPlayer.x < 0 && transform.localScale.x > 0))
            {
                flip();
            }
        }
        else
        {
            // Patrol between points
            Vector2 directionToPoint = (currentPoint.position - transform.position).normalized;
            rb.velocity = new Vector2(directionToPoint.x * speed, rb.velocity.y);

            // Ensure facing the correct patrol direction
            if ((directionToPoint.x > 0 && transform.localScale.x < 0) ||
                (directionToPoint.x < 0 && transform.localScale.x > 0))
            {
                flip();
            }

            // Check if the enemy has reached the current patrol point
            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
            {
                currentPoint = (currentPoint == pointA.transform) ? pointB.transform : pointA.transform;
            }
        }

        // Update animation speed based on movement
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }

    private void flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, .5f);
        Gizmos.DrawWireSphere(pointB.transform.position, .5f);
    }
}
