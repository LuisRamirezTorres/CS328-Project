using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CyberPsychosisHandler : MonoBehaviour
{

    private ProgressBar psychosisBar;
    public float psychosisLevel = 0f;
    public float increaseRate = 0.5f;
    public HealthHandler playerHealth;



    // Start is called before the first frame update
    void Start()
    {
        // Get the root visual element from the UIDocument
        UIDocument uiDoc = GetComponent<UIDocument>();
        VisualElement root = uiDoc.rootVisualElement;

        // Fetch the ProgressBar
        psychosisBar = root.Q<ProgressBar>(className: "unity-progress-bar");
        psychosisBar.Q(className: "unity-progress-bar__progress").style.backgroundColor = new StyleColor(Color.blue);

        if (psychosisBar != null)
        {
            psychosisBar.value = psychosisLevel;
            
            
        }
        else
        {
            Debug.LogError("ProgressBar not found. Check the class name and UI structure.");
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (psychosisLevel >= 100f)
        {
            playerHealth.health = 0;
            return;
        }

        if (psychosisBar != null)
        {
            // increase the psychosis level
            psychosisLevel += increaseRate * Time.deltaTime; 

            // clamp the value between 0 and 100
            psychosisLevel = Mathf.Clamp(psychosisLevel, 0f, 100f);

            
            psychosisBar.value = psychosisLevel;
        }
    }
}
