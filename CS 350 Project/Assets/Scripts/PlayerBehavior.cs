using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

//Josh Bonovich
//Project 2
//This script is to control the player's movement 
public class PlayerBehavior : MonoBehaviour
{
    public float jumpForce = 200f;
    public Sprite standard;
    public Sprite crouch;
    private Rigidbody2D rb;
    private PlayerActions pa;
    private HealthManager hm;
    private DisplayScore ds;
    private SpriteRenderer sr;
    private BoxCollider2D bc;

    private AudioSource playerAudio;

    public AudioClip chingSound; 

    bool canJump = true;
    bool crouching = false;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pa = new PlayerActions();
        hm = GameObject.FindObjectOfType<HealthManager>();
        ds = GameObject.FindObjectOfType<DisplayScore>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        playerAudio = GetComponent<AudioSource>();


    }
    private void OnEnable()
    {
        Debug.Log("Enable");
        
        pa.Default.Jump.performed += _ => Jump();
        pa.Default.Crouch.performed += _ => Crouch();
        pa.Enable();
        
    }
    private void OnDisable()
    {
        pa.Disable();
    }

    private void Jump()
    {
        Debug.Log("jump");
        if(canJump && !crouching)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;

            playerAudio.PlayOneShot(chingSound, 1f);
        }
    }

    private void Crouch()
    {
        if (!crouching && canJump)
        {
            crouching = true;
            bc.size = new Vector2(4.4f, 1.7f);
            sr.sprite = crouch;
            StartCoroutine(Uncrouch());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if jumping
        if (!canJump)
        {
            //if it landed on something
            if (collision.gameObject.transform.position.y < transform.position.y)
            {
                //allow jumping again
                canJump = true;
            }
        }

      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if hitting an obstacle
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            hm.TakeDamage();
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), collision);
        }
        //if hitting a yellow collectable
        if (collision.gameObject.CompareTag("YellowCollectible"))
        {
            ds.score += 100;
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), collision);
        }
        //if hitting a purple collectable
        if (collision.gameObject.CompareTag("PurpleCollectible"))
        {
            ds.score += 300;
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), collision);
        }
        //if hitting the finish
        if (collision.gameObject.CompareTag("End"))
        {
            ds.score += 2000;
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), collision);
            hm.win = true;
            hm.gameOver = true;
        }
        Destroy(collision.gameObject.transform.parent.gameObject);

    }

    IEnumerator Uncrouch()
    {
        yield return new WaitForSeconds(1.5f);
        crouching = false;
        bc.size = new Vector2(3.8f, 2.6f);
        sr.sprite = standard;
    }
}
