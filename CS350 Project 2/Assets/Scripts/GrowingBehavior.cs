using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingBehavior : MonoBehaviour
{

    public bool grown = false;
    [SerializeField] Sprite grownSprite;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!grown && collision.gameObject.CompareTag("Player"))
        {
            grown = true;
            GetComponent<SpriteRenderer>().sprite = grownSprite;
        }
    }
}
