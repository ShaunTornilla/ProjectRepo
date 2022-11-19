using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{

    [SerializeField] GameObject beginning;
    [SerializeField] GameObject worldSpace;
    private int line = 1;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        foreach (Transform temp in beginning.transform)
        {
            temp.gameObject.SetActive(false);
        }
        beginning.transform.GetChild(line).gameObject.SetActive(true);
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
            line++;
            if (line >= beginning.transform.childCount)
            {
                Time.timeScale = 1f;
                worldSpace.SetActive(true);
                beginning.SetActive(false);
            }
            else
            {
                beginning.transform.GetChild(line).gameObject.SetActive(true);
                beginning.transform.GetChild(line - 1).gameObject.SetActive(false);
            }
        }
    }
}
