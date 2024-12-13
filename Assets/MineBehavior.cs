using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject explosion;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }
}
