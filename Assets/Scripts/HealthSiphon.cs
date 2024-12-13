using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthSiphon : MonoBehaviour
{

    public float distanceBetween;
    public HealthHandler playerHealthHandler;
    public int decreaseRate;
    public GameObject player;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < distanceBetween)
        {
            
            playerHealthHandler.health -= (int)Time.deltaTime * 1;
        }

    }
}
