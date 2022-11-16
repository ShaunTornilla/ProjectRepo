using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{

    [SerializeField] GameObject beginning;
    [SerializeField] GameObject worldSpace;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    private void Update()
    {
        if (beginning.activeInHierarchy)
            Time.timeScale = 0f;
    }

    void OnContinue()
    {
        if(beginning.activeInHierarchy)
        {
            Time.timeScale = 1f;
            worldSpace.SetActive(true);
            beginning.SetActive(false);
        }
    }
}
