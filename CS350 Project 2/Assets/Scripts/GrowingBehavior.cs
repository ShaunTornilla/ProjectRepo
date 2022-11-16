using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingBehavior : MonoBehaviour
{
    private InteractChecker ic;
    public ParticleSystem particles;

    private AudioSource soundEffects;
    public AudioClip interacted;

    public bool grown = false;
    [SerializeField] Sprite grownSprite;
    

    private void Awake()
    {
        ic = GameObject.Find("InteractCheck").GetComponent<InteractChecker>();
        soundEffects = GetComponent<AudioSource>();
        
    }

    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!grown && ic.interacted && ic.pressed && collision.gameObject.CompareTag("Player"))
        {
            gameObject.tag = "Grown";
            grown = true;
            soundEffects.PlayOneShot(interacted, .5f);
            GetComponent<SpriteRenderer>().sprite = grownSprite;
            particles.Play();
            GameController.trees.Remove(gameObject);
            GameController.TreeGrown();
        }

        if (grown)
        {
            ic.Reset();
        }

    }
}
