using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static List<GameObject> trees;

    public static int totalTrees;
    public static int grownTrees = 0;
    public static bool change = false;
    public static bool gameOver = false;
    public static bool win = false;

    [SerializeField] string nextLevelName;
    // Start is called before the first frame update

    private AudioSource sound;
    [SerializeField] AudioClip winSound;
    
    [SerializeField] float maxCO2 = 100f;
    private float CO2 = 0f;
    private Text treeText;
    //private Text CO2Text;
    private GameObject gameOverObject;
    private GameObject pauseMenu;
    private GameObject nextLevelButton;

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public string levelName;

    //public Text CO2Text;

    void Start()
    {
        Time.timeScale = 1f;
        gameOver = false;
        win = false;
        change = false;
        grownTrees = 0;
        sound = GetComponent<AudioSource>();
        treeText = GameObject.FindGameObjectWithTag("TreeInfo").GetComponent<Text>();
        //CO2Text = GameObject.FindGameObjectWithTag("CO2Info").GetComponent<Text>();
        nextLevelButton = GameObject.FindGameObjectWithTag("NextLevel");
        gameOverObject = GameObject.FindGameObjectWithTag("GameOverInfo");
        gameOverObject.SetActive(false);
        pauseMenu = GameObject.FindGameObjectWithTag("PauseInfo");
        pauseMenu.SetActive(false);
        
        trees = new List<GameObject>(GameObject.FindGameObjectsWithTag("Ungrown"));
        //foreach (GameObject temp in trees)
            //Debug.Log(temp.name);
        totalTrees = trees.Count;
        treeText.text = "Trees Grown: " + grownTrees + "/" + totalTrees;
        //Debug.Log("Total Tree Count:" + totalTrees.ToString());
        StartCoroutine(CO2Counter());


    }

    private void LateUpdate()
    {

        SetUIBar(CO2, maxCO2);

        if(gameOver)
        {
            Text text = gameOverObject.GetComponent<Text>();
            gameOverObject.SetActive(true);
            if (win)
            {
                text.text = "You Win!";
                nextLevelButton.SetActive(true);
                GameObject.FindGameObjectWithTag("NextLevel").SetActive(true);
                PlayerPrefs.SetInt(levelName + "Beaten", 1);
            }
            else
            {
                text.text = "You Lose";
                nextLevelButton.SetActive(false);

            }
            Time.timeScale = 0f;
        }
        if(change)
        {
            change = false;
            //Debug.Log("Trees grown: " + grownTrees + "/" + totalTrees);
            treeText.text = "Trees Grown: " + grownTrees + "/" + totalTrees;
            if (grownTrees == totalTrees)
            {
                gameOver = true;
                win = true;
                sound.PlayOneShot(winSound, .5f);
                Debug.Log("All Trees Grown! You Win");
            }
        } 
    }

    public static void TreeGrown()
    {
        grownTrees++;
        change = true;
    }

    private IEnumerator CO2Counter()
    {
        while(!gameOver)
        {
            yield return new WaitForSeconds(0.1f + 0.1f * (grownTrees / totalTrees));
            CO2 += 0.1f;
            //CO2Text.text = (int) CO2 + "/" + maxCO2;
            if (CO2 >= maxCO2)
            {
                gameOver = true;
                //Debug.Log("CO2 Counter gameOver set to True");
            }

        }
    }

    public void OnPause()
    {
        if (!gameOver && !pauseMenu.activeInHierarchy)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        else if (!gameOver && pauseMenu.activeInHierarchy)
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }

    public void Restart()
    {
        GameObject.FindObjectOfType<GameManager>().Restart();
    }

    public void Exit()
    {
        Debug.Log("Load Main Menu");
        SceneManager.LoadScene("Main Menu");
    }

    public void Next()
    {
        if (nextLevelName != null)
            GameObject.FindObjectOfType<GameManager>().LoadLevel(nextLevelName);
        else
            Debug.Log("Load Next Level");
    }

    public void SetUIBar(float co2, float maxco2)
    {
        slider.value = co2 / maxco2;
        fill.color = gradient.Evaluate(co2 / maxco2);
    }
}
