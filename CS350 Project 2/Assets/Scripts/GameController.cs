using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static List<GameObject> trees;

    public static int totalTrees;
    public static int grownTrees = 0;
    public static bool change = false;
    // Start is called before the first frame update

    private AudioSource sound;
    public AudioClip winSound; 



    void Start()
    {
        sound = GetComponent<AudioSource>();

        trees = new List<GameObject>(GameObject.FindGameObjectsWithTag("Ungrown"));
        totalTrees = trees.Count;
        Debug.Log("Total Tree Count:" + totalTrees.ToString());
    }

    private void LateUpdate()
    {
        if(change)
        {
            change = false;
            Debug.Log("Trees grown: " + grownTrees + "/" + totalTrees);
            if (grownTrees == totalTrees)

                sound.PlayOneShot(winSound, .5f);

                Debug.Log("All Trees Grown! You Win");
        }
    }

    public static void TreeGrown()
    {
        grownTrees++;
        change = true;
    }




}
