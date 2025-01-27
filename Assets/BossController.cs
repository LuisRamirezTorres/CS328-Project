using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyHealth health = gameObject.GetComponent<EnemyHealth>();
        if (health.health <= 0)
        {
            rb.gravityScale = -.02f;
            circleCollider.enabled = false;
            
            SceneManager.LoadScene(3);
            return;
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
}
