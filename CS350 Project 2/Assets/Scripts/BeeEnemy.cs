
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeEnemy : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public GameObject playerObject;

    public float speed = 5f;
    public int radius = 5;
    private float startPosition;
    public bool movingLeft = true;
    public float knockbackForce;

    private AudioSource sound;
    public AudioClip beeSound;

    public AudioClip damageSound;
    public PlayerBehavior pb;

    // Start is called before the first frame update
    void Start()
    {
        //radius = 5;

        spriteRenderer = GetComponent<SpriteRenderer>();
        sound = GetComponent<AudioSource>();
        sound.clip = beeSound;
        sound.loop = true;
        startPosition = transform.position.y;
        pb = GameObject.Find("Player").GetComponent<PlayerBehavior>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }

    private void Move()
    {
        //Debug.Log("Difference: " + (startPosition - transform.position.y));
        if (movingLeft)
        {
            Debug.Log("Im running the moving left code");
            transform.Translate(new Vector2(0, -speed * Time.deltaTime));

            //Debug.Log("Start: " + startPosition + "\nTransform: " + transform.position.y);
            

            if (startPosition - transform.position.y >=  radius)
            {
                movingLeft = false;
            }
        }
        if (!movingLeft)
        {
            Debug.Log("Im running the moving right code");
            transform.Translate(new Vector2(0, speed * Time.deltaTime));

            if (transform.position.y - startPosition  <= radius)
            {
                movingLeft = false;
            }
        }

        if (pb.gameObject.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
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
        Debug.Log("In Area of Bee");
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


