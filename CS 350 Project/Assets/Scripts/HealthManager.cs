﻿/*
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

    public GameObject gameOverText;
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
            gameOverText.SetActive(true);
            //ND
            goText.text = "Game Over!\nFinal Score: " + GameObject.FindObjectOfType<DisplayScore>().score + "\nPress R to Restart or E to quit";
            Time.timeScale = 0;
            //Press R to restart if game is over
            //ND
            /*if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            */
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
            Application.Quit();
            Debug.Log("Quitting");

        }
    }

    void OnForceQuit()
    {
        Application.Quit();
        Debug.Log("Force Quitting");
    }

}
