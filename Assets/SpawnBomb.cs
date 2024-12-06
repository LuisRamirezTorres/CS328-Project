using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpawnBomb : MonoBehaviour
{
    // Start is called before the first frame update
    private float timer = 0.0f;
    public GameObject bomb;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 5)
        {
            Instantiate(bomb, transform.position, Quaternion.identity);
            timer = 0;
        }
    }
}
