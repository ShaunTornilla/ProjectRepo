using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgehogEnemy : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public float speed;
    public int radius = 7;
    private float startPosition;
    private bool movingLeft = true;
    public float knockbackForce;
    public int lowerLimit = -20;

    private AudioSource sound;
    public AudioClip damageSound;
    public PlayerBehavior pb;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        sound = GetComponent<AudioSource>();
        startPosition = transform.position.x;
        pb = GameObject.Find("Player").GetComponent<PlayerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        //despawn if enemy falls off screen
        if (transform.position.y < lowerLimit)
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        if (movingLeft)
        {
            transform.Translate(new Vector2(-speed * Time.deltaTime, 0));

            if (startPosition - transform.position.x >= radius)
            {
                spriteRenderer.flipX = true;
                movingLeft = false;
            }
        }
        if (!movingLeft)
        {
            transform.Translate(new Vector2(speed * Time.deltaTime, 0));

            if (transform.position.x - startPosition >= radius)
            {
                spriteRenderer.flipX = false;
                movingLeft = true;
            }
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
