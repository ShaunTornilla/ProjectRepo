using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

//Josh Bonovich
//Project 2
//This script is to control the player's movement during the tutorial
public class TutorialPlayerBehavior : MonoBehaviour
{
    public float jumpForce = 200f;
    private Rigidbody2D rb;
    private PlayerActions pa;
    private BoxCollider2D bc;
    private Animator am;

    private AudioSource playerAudio;

    public AudioClip chingSound;
    public AudioClip collectibleSound;


    public Animator playerAnimator;


    public bool TutorialJumpAllowed = false;
    public bool TutorialCrouchAllowed = false;

    bool canJump = true;
    bool crouching = false;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pa = new PlayerActions();
        bc = GetComponent<BoxCollider2D>();
        am = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();


    }
    private void OnEnable()
    {
        //Debug.Log("Enable");

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
        if (canJump && !crouching && TutorialJumpAllowed)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;

            playerAudio.PlayOneShot(chingSound, 1f);
            TutorialJumpAllowed = false;
        }
    }

    private void Crouch()
    {

        if (!crouching && canJump && TutorialCrouchAllowed)
        {
            crouching = true;
            bc.size = new Vector2(4.4f, 1.7f);
            am.SetBool("Crouching", true);
            StartCoroutine(Uncrouch());
            TutorialCrouchAllowed = false;
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



        if (collision.gameObject.CompareTag("CollectableTutorial"))
        {
            playerAudio.PlayOneShot(collectibleSound, 1f);
        }

        Destroy(collision.gameObject.transform.parent.gameObject);

    }

    IEnumerator Uncrouch()
    {
        yield return new WaitForSeconds(1.5f);
        crouching = false;
        bc.size = new Vector2(3.8f, 2.6f);
        am.SetBool("Crouching", false);
    }
}
