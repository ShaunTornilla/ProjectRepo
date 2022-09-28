using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Josh Bonovich
//Project 2
//Ability to spawn obstacles randomly
public class SpawningBehavior : MonoBehaviour
{
    //public GameObject[] spawnables;
    public GameObject[] obstacles;
    public GameObject[] obstacleWithCollectable;
    public GameObject[] collectable;
    public GameObject end;
    public float minTime;
    public float maxTime;
    private HealthManager hm;
    public float totalTime = 120f;
    public float currentTime;

    private void Start()
    {
        hm = GameObject.FindObjectOfType<HealthManager>();
        StartCoroutine(Spawning());
        StartCoroutine(Timer());
        currentTime = 0;
    }

    private void Spawn()
    {
        int roll = Random.Range(0, 10);
        if(currentTime<totalTime)
        {
            if (roll >= 0 && roll < 2)
            {
                Instantiate(obstacles[Random.Range(0, obstacles.Length)], transform.position, Quaternion.identity);
            }
            else if (roll >= 2 && roll < 8)
            {
                Instantiate(obstacleWithCollectable[Random.Range(0, obstacleWithCollectable.Length)], transform.position, Quaternion.identity);
            }
            else if (roll >= 8 && roll < 10)
            {
                Instantiate(collectable[Random.Range(0, collectable.Length)], transform.position, Quaternion.identity);
            }
        }
        //Instantiate(spawnables[Random.Range(0, spawnables.Length)], transform.position, Quaternion.identity);

    }

    private IEnumerator Spawning()
    {
        while(!hm.gameOver && currentTime<totalTime)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            Spawn();
        }
        if(currentTime>=totalTime)
        {
            Instantiate(end, transform.position, Quaternion.identity);
        }
    }

    private IEnumerator Timer()
    {
        while(currentTime<totalTime)
        {
            yield return new WaitForSeconds(0.1f);
            currentTime += 0.1f;
        }
    }

}
