using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ElectricBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public Collider2D obj_collider;
    private float timer = 0.0f;
    private bool enable_timer = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enable_timer)
        {
            timer += Time.deltaTime;

            if (timer > 5)
            {
                obj_collider.enabled = true;
                enable_timer = false;
                timer = 0.0f;
            }
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject hud = GameObject.FindGameObjectWithTag("HealthHUD");
            CharacterController2D pm = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
            //pm.shock_multiplier = 0;
            hud.GetComponent<HealthHandler>().health -= 5;
            obj_collider.enabled = false;
            enable_timer = true;

        }
    }
}
