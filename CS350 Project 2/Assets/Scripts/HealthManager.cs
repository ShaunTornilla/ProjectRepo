using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int health;
    public int maxHealth = 5;

    public List<Image> healthBar;
    public Sprite greenHealth;
    public Sprite redHealth;


   // public GameObject gameOverText;
   // public GameObject newHighScoreText;
    //Edits by Josh Bonovich tagged with "ND"
   // public Text goText;

    //ND
    private void Start()
    {
        Time.timeScale = 1;
        health = maxHealth;
    }

    void Update()
    {
        //If health is somehow more than max health, set health to max health
        if (health > maxHealth)
        {
            health = maxHealth;
        }


        for (int i = 0; i < healthBar.Count; i++)
        {
            //Display green health bar chunks for when health is above 40%, red health bar chunks when health is at or below 40%, and disables top health bar upon taking damage
            if (i < health && health > 0.4 * maxHealth)
            {
                healthBar[i].enabled = true;
                healthBar[i].sprite = greenHealth;
            }
            else if (i < health && health <= 0.4 * maxHealth)
            {
                healthBar[i].enabled = true;
                healthBar[i].sprite = redHealth;
            }
            else
            {
                healthBar[i].enabled = false;
            }

        }

        if (health <= 0)
        {
            GameController.gameOver = true;

        }


    }

    public void TakeDamage()
    {
        health--;
    }

}
