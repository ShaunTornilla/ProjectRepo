using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Josh Bonovich
//Project 2
//Holds the backgrounds for the RepeatingBackground script
public class BackgroundCounter : MonoBehaviour

    
{
    private int backgroundID;

    public Sprite[] backgrounds;
    // Start is called before the first frame update
    void Start()
    {
        backgroundID = 1;
    }


    public Sprite GetSprite()
    {
        backgroundID++;
        if (backgroundID >= backgrounds.Length)
            backgroundID = 0;
        return backgrounds[backgroundID];
    }
}
