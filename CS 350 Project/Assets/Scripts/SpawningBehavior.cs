using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Josh Bonovich
//Project 2
//Ability to spawn obstacles randomly
public class SpawningBehavior : MonoBehaviour
{
    public GameObject[] spawnables;
    public float minTime;
    public float maxTime;
    private HealthManager hm;

    private void Start()
    {
        hm = GameObject.FindObjectOfType<HealthManager>();
        StartCoroutine(Spawning());
    }

    private void Spawn()
    {
        Instantiate(spawnables[Random.Range(0, spawnables.Length)], transform.position, Quaternion.identity);
    }

    private IEnumerator Spawning()
    {
        while(!hm.gameOver)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            Spawn();
        }
    }


}
