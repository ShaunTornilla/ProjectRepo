using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lockLevel : MonoBehaviour
{

    private int previousLevelBeaten;
    public string previousLevel;

    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        previousLevelBeaten = PlayerPrefs.GetInt(previousLevel + "Beaten", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (previousLevelBeaten == 1)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }
}
