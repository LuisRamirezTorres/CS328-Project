using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CyberPsychosisHandler : MonoBehaviour
{

    private VisualElement psychosisBar;
    public float psychosisLevel = 0f;



    // Start is called before the first frame update
    void Start()
    {
        
        UIDocument uiDoc = GetComponent<UIDocument>();
        VisualElement element = uiDoc.rootVisualElement;
        psychosisBar = element.Q(className: "unity-progress-bar__progress");
        psychosisBar.style.backgroundColor = Color.blue;


    }

    // Update is called once per frame
    void Update()
    {

        UIDocument uiDoc = GetComponent<UIDocument>();
        VisualElement element = uiDoc.rootVisualElement;
        psychosisBar = element.Q(className: "unity-progress-bar__progress");
        psychosisBar.style.backgroundColor = Color.blue;
        ProgressBar pb = (ProgressBar)element.Q(className: "unity-bar__progress");
        pb.value = psychosisLevel;


    }
}
