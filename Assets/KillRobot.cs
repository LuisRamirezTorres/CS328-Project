using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillRobot : MonoBehaviour
{
    // Start is called before the first frame update
    EnemyHealth health;
    void Start()
    {
        health = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health.currentHealth <= 0 || health.health <= 0)
        {
            Debug.Log("Destroying!");
            Destroy(gameObject);
        }
    }
}
