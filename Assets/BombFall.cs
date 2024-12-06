using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFall : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private float timer = 0.0f;
    public GameObject explosion;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        timer += Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }
}
