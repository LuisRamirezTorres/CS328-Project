using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Animator anim;
    public CharacterController2D controller;

    float horizontalMove = 0f;

    public float runSpeed = 40f;
    bool jump = false;

    public GameObject punchPoint;
    public GameObject runPunchPoint;
    public float radius;
    public LayerMask enemies;
    public float damage; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        anim.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            anim.SetBool("isJumping", true);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetBool("isPunching", true);
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
            enemyGameObject.GetComponent<EnemyHealth>().health -= damage;   // damage is applied to an enemy in the "enemies" layer
        }
    }

    public void runPunch()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(runPunchPoint.transform.position, radius, enemies);

        foreach (Collider2D enemyGameObject in enemy) {
            Debug.Log("Enemy Hit");
            enemyGameObject.GetComponent<EnemyHealth>().health -= damage;   // damage is applied to an enemy in the "enemies" layer
        }
    }


    public void endPunch()
    {
        anim.SetBool("isPunching", false);
    }

    void FixedUpdate()
    {

        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);    // Move our player
        jump = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(punchPoint.transform.position, radius);
        Gizmos.DrawWireSphere(runPunchPoint.transform.position, radius);
    }

}
