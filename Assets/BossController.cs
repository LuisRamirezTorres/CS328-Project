using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class BossController : MonoBehaviour
{
    // Start is called before the first frame update
    public ProjectileBehavior projectile;
    public Transform offset;
    private float cooldown = 3.0f;
    public GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scale = transform.localScale;
        if (player.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1;
        }
        else
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
        cooldown -= Time.deltaTime;
        if (cooldown <= 0)
        {
            cooldown = 3.0f;
            Object proj = Instantiate(projectile, offset.position, transform.rotation);
            if (transform.localScale.x < 0)
            {
                proj.GetComponent<ProjectileBehavior>().left = false;
            }
        }
    }
}
