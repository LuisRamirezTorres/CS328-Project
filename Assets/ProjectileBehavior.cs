using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 3.5f;
    public bool left = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (left)
        {
            transform.position += -transform.right * Time.deltaTime * speed;
        }
        else
        {
            transform.position += transform.right * Time.deltaTime * speed;

        }
    }
}
