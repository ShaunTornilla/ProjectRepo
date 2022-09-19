using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject howToPlayMenu;
    public GameObject mainMenu;

    public void Start()
    {
        Time.timeScale = 1;
    }
    public void Begin()
    {
        SceneManager.LoadScene("Game");
    }
    public void HowToPlay()
    {
        howToPlayMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void ReturnToMenu()
    {
        howToPlayMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void ExitToDesktop()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}
