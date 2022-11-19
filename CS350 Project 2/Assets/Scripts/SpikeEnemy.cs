using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeEnemy : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private GameObject playerObject;

    public float speed;
    public int visionRadius = 7;
    private float startPosition;
    private bool movingLeft = true;
    public float knockbackForce;
    public int lowerLimit = -20;

    private AudioSource sound;
    public PlayerBehavior pb;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        sound = GetComponent<AudioSource>();
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


        //if (transform.position.x - playerObject.transform.position.x <= visionRadius && transform.position.x - playerObject.transform.position.x > 0)
        if (transform.position.x - playerObject.transform.position.x <= visionRadius && playerObject.transform.position.y - transform.position.y <= visionRadius && transform.position.x - playerObject.transform.position.x > 0 &&
            playerObject.transform.position.y - transform.position.y > 0)
        {
            transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
            spriteRenderer.flipX = false;
            movingLeft = false;

            sound.Play();

        }
        //if player is within vision radius and to the right of the enemy, move right
        //else if (playerObject.transform.position.x - transform.position.x <= visionRadius && playerObject.transform.position.x - transform.position.x > 0)
        else if (playerObject.transform.position.x - transform.position.x <= visionRadius && playerObject.transform.position.y - transform.position.y <= visionRadius && playerObject.transform.position.x - transform.position.x > 0 &&
                playerObject.transform.position.y - transform.position.y > 0)
        {
            transform.Translate(new Vector2(speed * Time.deltaTime, 0));
            spriteRenderer.flipX = true;
            movingLeft = true;

            sound.Play();


        }
        else
        {
            sound.Stop();
        }
    }

    // Enemy Collision
    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Debug.Log("Collided with trigger!");

        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Collided with enemy!");

            pb = collision.GetComponent<PlayerBehavior>();

            if (transform.position.x > collision.transform.position.x)
            {
                pb.knockbackDirection = new Vector2(-2, 1).normalized;
            }
            else
            {
                pb.knockbackDirection = new Vector2(2, 1).normalized;
            }

            //sound.PlayOneShot(damageSound, .5f);
            pb.Knockback();

        }
    }
}
