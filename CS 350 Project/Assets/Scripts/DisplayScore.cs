/*
 * Josh Beck
 * Project 2
 * Displays and updates score UI
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{

    public Text textbox;
    public Text highScoreText;

    public int score = 0;
    private int highScore;

    // Start is called before the first frame update
    void Start()
    {
        //set text component reference
        textbox = GetComponent<Text>();
        textbox.text = "Score: 0";
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "HighScore: " + highScore;
    }

    // Update is called once per frame
    void Update()
    {

        textbox.text = "Score: " + score;
        if(score>highScore)
        {
            highScoreText.text = "HighScore: " + score;
        }

    }
}
