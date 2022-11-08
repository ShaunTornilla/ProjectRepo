using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingBehavior : MonoBehaviour
{
    public InteractChecker ic;


    public bool grown = false;
    [SerializeField] Sprite grownSprite;

    private void Awake()
    {
        ic = GameObject.FindGameObjectWithTag("InteractCheck").GetComponent<InteractChecker>();

    }

    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!grown && ic.interacted && ic.pressed && collision.gameObject.CompareTag("Player"))
        {
            gameObject.tag = "Grown";
            grown = true;
            GetComponent<SpriteRenderer>().sprite = grownSprite;
            GameController.trees.Remove(gameObject);
            GameController.TreeGrown();
        }

        if (grown)
        {
            ic.Reset();
        }

    }
}
