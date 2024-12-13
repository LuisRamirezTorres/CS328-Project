using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BossControllerSmasher : MonoBehaviour
{
    // Start is called before the first frame update
    public ProjectileBehavior projectile;
    public Transform offset;
    private float cooldown = 1.0f;
    public GameObject player;
    public float speed = 2.0f;
    public bool flip;
    public Collider2D circleCollider;
    public Rigidbody2D rb;
    public float distanceBetween;
    public GameObject punchPoint;
    public float radius;
    public HealthHandler playerHealthHandler;
    public LayerMask players;
    
    


    private Animator anim;
    private float distance;

    void Start()
    {
        anim = GetComponent<Animator>();
       
        anim.SetFloat("Speed", speed);
    }

    // Update is called once per frame
    void Update()
    {
        EnemyHealth health = gameObject.GetComponent<EnemyHealth>();
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (health.health <= 0)
        {
            anim.SetFloat("Speed", 0f);
            rb.gravityScale = -.02f;
            circleCollider.enabled = false;
            SceneManager.LoadScene(0);
            return;
        }

        if(playerHealthHandler.health <= 0)
        {
            SceneManager.LoadScene(0);
        }

        if (distance < distanceBetween)
        {
            if (distance < .9f)
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


        Vector3 scale = transform.localScale;
        if (player.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
            transform.Translate(speed * Time.deltaTime * -1, 0, 0);
        }



        transform.localScale = scale;
        cooldown -= Time.deltaTime;

        if (cooldown <= 0)
        {
            cooldown = 1.5f;
            Object proj = Instantiate(projectile, offset.position, transform.rotation);
            if (transform.localScale.x < 0)
            {
                proj.GetComponent<ProjectileBehavior>().left = false;
            }
        }
    }

    public void punch()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(punchPoint.transform.position, radius, players);

        foreach (Collider2D playerGameObject in player)
        {
            Debug.Log("Player Hit");
            
            playerHealthHandler.health -= 25;
        }
    }

    public void endPunch()
    {
        anim.SetBool("isPunching", false);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(punchPoint.transform.position, radius);
    }

}
