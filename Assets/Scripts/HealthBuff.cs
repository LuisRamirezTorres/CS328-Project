using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBuff : MonoBehaviour
{
    
    public int increaseAmount;


    

    private AudioManager audioManager;

    private void Start()
    {
         
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject hud = GameObject.FindGameObjectWithTag("HealthHUD");
        audioManager.PlaySFX(audioManager.obtainStim);
        hud.GetComponent<HealthHandler>().health += increaseAmount;
        Destroy(gameObject);

        // SHOULD RESIZE THE PROGRESS BAR BUT NOT WORKING PROPERLY????
        UIDocument ui_doc = gameObject.GetComponent<UIDocument>();
        VisualElement ve = ui_doc.rootVisualElement;
        ProgressBar pb = (ProgressBar)ve.Q(className: "unity-progress-bar");
        pb.style.width = 20;


        //Destroy(gameObject);

    }
}
