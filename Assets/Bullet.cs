using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] 
    private float speed;
    
    [SerializeField] 
    private GameObject impactEffect;    

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log("Bullet hit " + hitInfo.name);

        Instantiate(impactEffect, transform.position, transform.rotation);
        
        Destroy(gameObject);
    }
    
}
