using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// IMPORTANT NOTE: THIS SCRIPT IS A MODIFIED (MODIFIED TO FIT PROJECT NEEDS) VERSION OF BRACKEY'S CHARACTER 2D CONTROLLER SCRIPT FROM YOUTUBE


public class PlayerMovement : MonoBehaviour
{

    public Animator anim;
    public CharacterController2D controller;
    public Rigidbody2D rb;
    public HealthHandler playerHealth;

    [Header("Colliders")]
    public Collider2D playerBoxCollider;
    public Collider2D playerCircleCollider;
    
    float horizontalMove = 0f;
    private float currentHealth;

    public float runSpeed = 40f;
    bool jump = false;
    
    [Header("Punch Points")]
    public GameObject punchPoint;
    public GameObject runPunchPoint;
    
    [Header("Enemies & Damage")]
    public float radius;
    public LayerMask enemies;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        //playerHealth = GetComponent<HealthHandler>();

        currentHealth = playerHealth.health;

    }

    // Update is called once per frame
    void Update()
    {

        if (playerHealth.health < currentHealth)
        {
            currentHealth = playerHealth.health;
            anim.SetTrigger("isAttacked");
        }

        if (currentHealth <= 0)
        {
            anim.SetBool("isDead", true);
            playerBoxCollider.enabled = false;
            playerCircleCollider.enabled = false;
            rb.gravityScale = .03f;
            return;
        }




        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        anim.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            anim.SetBool("isDoubleJumping", false);
            anim.SetBool("isJumping", true);
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.J))
        {
            anim.SetBool("isPunching", true);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetBool("isShooting", true);
        }

    }

    public void onLanding()
    {
        anim.SetBool("isJumping", false);
    }


    public void punch()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(punchPoint.transform.position, radius, enemies);
    
        foreach (Collider2D enemyGameObject in enemy) {
            Debug.Log("Enemy Hit");
            enemyGameObject.GetComponent<EnemyHealth>().TakeDamage(damage);   // damage is applied to an enemy in the "enemies" layer
        }
    }

    public void runPunch()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(runPunchPoint.transform.position, radius, enemies);

        foreach (Collider2D enemyGameObject in enemy) {
            Debug.Log("Enemy Hit");
            enemyGameObject.GetComponent<EnemyHealth>().TakeDamage(damage);   // damage is applied to an enemy in the "enemies" layer
        }
    }


    public void endPunch()
    {
        anim.SetBool("isPunching", false);
    }


    public void endShooting()
    {
        anim.SetBool("isShooting", false);
    }

    void FixedUpdate()
    {

        controller.Move(horizontalMove * Time.fixedDeltaTime, jump, false);    // Move our player
        jump = false;
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(punchPoint.transform.position, radius);
        Gizmos.DrawWireSphere(runPunchPoint.transform.position, radius);
    }

}
