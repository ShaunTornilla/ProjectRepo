using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeEnemy : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private GameObject playerObject;

    public float speed = 5f;
    public int radius = 5;
    private float startPosition;
    private bool movingLeft = true;
    public float knockbackForce;

    private AudioSource sound;
    public AudioClip damageSound;
    public PlayerBehavior pb;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        sound = GetComponent<AudioSource>();
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
        if (movingLeft)
        {
            transform.Translate(new Vector2(0, -speed * Time.deltaTime));

            if (startPosition - transform.position.y >=  radius)
            {
                movingLeft = false;
            }
        }
        if (!movingLeft)
        {
            transform.Translate(new Vector2(0, speed * Time.deltaTime));

            if (transform.position.y - startPosition  >= radius)
            {
                movingLeft = true;
            }
        }

        if (playerObject.gameObject.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
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
