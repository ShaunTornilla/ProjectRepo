using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Josh Bonovich
//Project 2
//Controls the behavior of the obstacle
public class ObstacleBehavior : RepeatingBackground
{
    protected override void Offscreen(ref Vector3 pos)
    {
        if (transform.GetChild(0).CompareTag("Obstacle"))
        {
            GameObject.FindObjectOfType<DisplayScore>().score += 50;
            
        }
        Destroy(gameObject);
    }
}
