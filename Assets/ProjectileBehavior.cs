using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 8f;
    public bool left = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!left)
        {
            transform.position += -transform.right * Time.deltaTime * speed;
        }
        else
        {
            transform.position += transform.right * Time.deltaTime * speed;

        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit");
            GameObject hud = GameObject.FindGameObjectWithTag("HealthHUD");
            hud.GetComponent<HealthHandler>().health -= 20;
        }
        Destroy(gameObject);
    }
}
