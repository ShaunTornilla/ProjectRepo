using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Josh Bonovich
//Project 2
//This script is to control the background and will later also be used for obstacles via inheritence 
public class RepeatingBackground : MonoBehaviour
{
    public float scrollSpeed;

    public float ScrollWidth = 17.74f;

    public static int backgroundID;

    public Sprite[] backgrounds;


    private void Awake()
    {
        backgroundID = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = transform.position;

        pos.x -= scrollSpeed * Time.deltaTime;

        if (transform.position.x < -ScrollWidth)
        {
            Offscreen(ref pos);
        }

        transform.position = pos;
    }

    protected virtual void Offscreen(ref Vector3 pos)
    {
        pos.x += 2 * ScrollWidth;
        backgroundID++;
        if (backgroundID>=backgrounds.Length)
        {
            backgroundID = 0;
        }
        GetComponent<SpriteRenderer>().sprite = backgrounds[backgroundID];
    }
}
