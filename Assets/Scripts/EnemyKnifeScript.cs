using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnifeScript : MonoBehaviour
{
    private GameObject player;

    private Rigidbody2D rb;

    public float force;
    private float timer; 


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force; ;

        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation + 180);

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 10)
        {
            Destroy(gameObject);
        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject hud = GameObject.FindGameObjectWithTag("HealthHUD");
            hud.GetComponent<HealthHandler>().health -= 10;

        }
        Destroy(gameObject);
    }

}
