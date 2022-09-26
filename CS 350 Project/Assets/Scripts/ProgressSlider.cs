using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressSlider : MonoBehaviour
{
    //public UI variables that this script modifies
    public Gradient gradient;
    public Slider slider;
    public Image fill;
    private SpawningBehavior sb;
    // Start is called before the first frame update
    //Set the value to 0 as well as initialize the gradient
    void Start()
    {
        slider.value = 0;
        fill.color = gradient.Evaluate(1f);
        sb = GameObject.FindObjectOfType<SpawningBehavior>();
    }

    // Update is called once per frame
    //Fills the bar based on current score, the text and bar color based on a normalized value from the slider, and then update the text to the current threat level
    void Update()
    {
        //Debug.Log(GameControllerBehavior.score);
        slider.value = Mathf.Clamp(sb.currentTime, 0, sb.totalTime);
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
