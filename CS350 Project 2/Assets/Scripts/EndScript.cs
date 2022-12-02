using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndScript : MonoBehaviour
{
    [SerializeField] string nextLevelName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Next()
    {
        if (nextLevelName != null)
            SceneManager.LoadScene("Main Menu");
        else
            Debug.Log("Load Next Level");
    }
}
