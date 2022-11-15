using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameObject pauseMenu;
    public GameObject mainMenu;

    public bool pause = false;
    public bool startClock = false;

    // Variable to keep track of current level
    public string CurrentLevelName = string.Empty;

    public static GameManager instance;

    /*#region This code makes this class a Singleton
    private void Awake()
    {
        if (instance == null)
        {
            // This is referring to this exact script
            instance = this;

            // Make sure this game manager persists across scenes
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            Debug.LogError("Trying to instantiate a second" + "instance of singleton Game Manager");
        }
    }
    #endregion*/

    // Methods to load and unload scenes
    public void LoadLevel(string levelName)
    {
        Time.timeScale = 1f;

        if (CurrentLevelName.CompareTo(string.Empty) != 0)
            unloadCurrentLevel();

        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);

        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to load level " + levelName + ".");
            return;
        }

        CurrentLevelName = levelName;

    }

    // Methods to load and unload scenes
    public void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);

        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to unload level " + levelName + ".");
            return;
        }

    }

    public void unloadCurrentLevel()
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(CurrentLevelName);

        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to unload level " + CurrentLevelName + ".");
            return;
        }


    }

    public void Restart()
    {
        LoadLevel(CurrentLevelName);
    }

    // Methods to pause and unpause
    public void Pause()
    {
        pause = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    // Methods to pause and unpause
    public void Unpause()
    {
        pause = false;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    // Methods to pause and unpause
    public void Menu()
    {
        Cursor.lockState = CursorLockMode.None;
        startClock = false;
        Time.timeScale = 0f;
        pauseMenu.SetActive(false);
        mainMenu.SetActive(true);

    }


}
