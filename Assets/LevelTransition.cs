using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public int sceneIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D called.");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Condition met.  Loading scene.");
            SceneManager.LoadScene(sceneIndex);   
        }
    }
}
