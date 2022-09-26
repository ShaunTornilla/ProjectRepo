/*
 * Josh Beck
 * Project 2
 * Maintains health system and allows visual UI to update as damage is taken
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public List<Image> hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public bool gameOver = false;
    public bool win = false;

    public GameObject gameOverText;
    public GameObject newHighScoreText;
    //Edits by Josh Bonovich tagged with "ND"
    public Text goText;

    //ND
    private void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        //If health is somehow more than max health, set health to max health
        if (health > maxHealth)
        {
            health = maxHealth;
        }


        for (int i = 0; i < hearts.Count; i++)
        {
            //Display full or empty heart sprite based on current health
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            //Show the number of hearts equal to current max health
            if (i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        if (health <= 0)
        {
            gameOver = true;
            
            //ND
            
            
            //Press R to restart if game is over
            //ND
            /*if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            */
        }

        if(gameOver)
        {
            Time.timeScale = 0;
            if(win)
            {
                goText.text = "You Win!\nFinal Score: " + GameObject.FindObjectOfType<DisplayScore>().score + "\nPress R to Restart or E to return to the main menu";
            }
            else
            {
                goText.text = "Game Over!\nFinal Score: " + GameObject.FindObjectOfType<DisplayScore>().score + "\nPress R to Restart or E to return to the main menu";
            }

            if (PlayerPrefs.GetInt("HighScore") < GameObject.FindObjectOfType<DisplayScore>().score)
            {
                newHighScoreText.SetActive(true);
                PlayerPrefs.SetInt("HighScore", GameObject.FindObjectOfType<DisplayScore>().score);
            }
            gameOverText.SetActive(true);
        }

    }

    public void TakeDamage()
    {
        health--;
    }

    //ND to work with new input system
    void OnRestart()
    {
        if (gameOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnQuit()
    {
        if(gameOver)
        {
            SceneManager.LoadScene("Main Menu");
            Debug.Log("Quitting");

        }
    }

    void OnForceQuit()
    {
        Application.Quit();
        Debug.Log("Force Quitting");
    }

}
