using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Josh Bonovich
//Project 2
//Ability to spawn obstacles randomly
public class SpawningBehavior : MonoBehaviour
{
    public GameObject[] spawnables;
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
        if(currentTime<totalTime)
        Instantiate(spawnables[Random.Range(0, spawnables.Length)], transform.position, Quaternion.identity);

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
