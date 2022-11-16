using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeEnemy : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public GameObject playerObject;

    public float speed;
    public int visionRadius;
    private float startPosition;
    private bool movingLeft = true;
    public float knockbackForce;
    public int lowerLimit = -20;

    private AudioSource sound;
    public AudioClip walkingSound; 


    public PlayerBehavior pb;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        visionRadius = 15;
        spriteRenderer = GetComponent<SpriteRenderer>();
        sound = GetComponent<AudioSource>();
        sound.clip = walkingSound;
        sound.loop = true;
        startPosition = transform.position.x;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        pb = playerObject.GetComponent<PlayerBehavior>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        animator.SetBool("playerInRange", Mathf.Abs(playerObject.transform.position.x - transform.position.x) <= visionRadius);

        //despawn if enemy falls off screen
        if (transform.position.y < lowerLimit)
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        //if player is within vision radius and to the left of the enemy, move left
        if (transform.position.x - playerObject.transform.position.x <= visionRadius && transform.position.x - playerObject.transform.position.x > 0)
        {
            transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
            spriteRenderer.flipX = false;
            movingLeft = false;
        }
        //if player is within vision radius and to the right of the enemy, move right
        else if (playerObject.transform.position.x - transform.position.x <= visionRadius && playerObject.transform.position.x - transform.position.x > 0)
        {
            transform.Translate(new Vector2(speed * Time.deltaTime, 0));
            spriteRenderer.flipX = true;
            movingLeft = true;
        }
    }

    // Enables Enemy Audio when it touches player's circle collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SurroundingCollider"))
        {
            EnableEnemyAudio();
        }
    }

    // Constantly checks for Player hitbox when in circle, damage when collided
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            //Debug.Log("Collided with enemy!");

            pb.playerCollider.enabled = !pb.playerCollider.enabled;

            StartCoroutine(pb.Invincibility());


            if (transform.position.x > collision.transform.position.x)
            {
                pb.knockbackDirection = new Vector2(-2, 1).normalized;
            }
            else
            {
                pb.knockbackDirection = new Vector2(2, 1).normalized;
            }

            pb.Knockback();

        }
    }

    // Disables Audio when outside player circle
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("SurroundingCollider"))
        {
            DisableEnemyAudio();
        }
    }

    public void EnableEnemyAudio()
    {
        //Debug.Log("Enabling Audio");

        sound.volume = .5f;
        sound.Play();
    }

    public void DisableEnemyAudio()
    {
        //Debug.Log("Audio Stopped");
        sound.Stop();

    }

}




