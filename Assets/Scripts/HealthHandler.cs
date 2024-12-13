using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private VisualElement bar;
    public float health = 100;
    void Start()
    {
        UIDocument ui_doc = gameObject.GetComponent<UIDocument>();
        VisualElement ve = ui_doc.rootVisualElement;
        bar = ve.Q(className: "unity-progress-bar__progress");
        bar.style.backgroundColor = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        UIDocument ui_doc = gameObject.GetComponent<UIDocument>();
        VisualElement ve = ui_doc.rootVisualElement;
        bar = ve.Q(className: "unity-progress-bar__progress");
        bar.style.backgroundColor = Color.red;
        ProgressBar pb = (ProgressBar)ve.Q(className: "unity-progress-bar");
        pb.value = health;
    }
}
