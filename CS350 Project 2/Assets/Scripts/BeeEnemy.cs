using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeEnemy : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public float speed = 5f;
    public int radius = 5;
    private float startPosition;
    private bool movingLeft = true;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        startPosition = transform.position.x;
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
            transform.Translate(new Vector2(-speed * Time.deltaTime, 0));

            if (startPosition - transform.position.x >=  radius)
            {
                spriteRenderer.flipX = true;
                movingLeft = false;
            }
        }
        if (!movingLeft)
        {
            transform.Translate(new Vector2(speed * Time.deltaTime, 0));

            if (transform.position.x - startPosition  >= radius)
            {
                spriteRenderer.flipX = false;
                movingLeft = true;
            }
        }
    }

}
