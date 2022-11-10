using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustBackgroundTransparency : MonoBehaviour
{

    public float maxAlpha;
    public float minAlpha;
    private SpriteRenderer backgroundImage;
    private Color currentColor;

    // Start is called before the first frame update
    void Start()
    {

        if (maxAlpha > 255)
        {
            maxAlpha = 255f;
        }
        if (minAlpha < 0)
        {
            minAlpha = 0f;
        }

        backgroundImage = GetComponent<SpriteRenderer>();

        //changes these from standard RGBA values to a 0-1 scale
        maxAlpha = maxAlpha / 255;
        minAlpha = minAlpha / 255;

    }

    // Update is called once per frame
    void Update()
    {

        currentColor = backgroundImage.color;
        currentColor.a = maxAlpha - (maxAlpha - minAlpha) * GameController.grownTrees / GameController.totalTrees;
        backgroundImage.color = currentColor;

    }
}
