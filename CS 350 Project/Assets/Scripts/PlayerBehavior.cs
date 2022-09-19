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
    private Rigidbody2D rb;
    private PlayerActions pa;
    private HealthManager hm;
    private DisplayScore ds;
    bool canJump = true;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pa = new PlayerActions();
        hm = GameObject.FindObjectOfType<HealthManager>();
        ds = GameObject.FindObjectOfType<DisplayScore>();

    }
    private void OnEnable()
    {
        Debug.Log("Enable");
        
        pa.Default.Jump.performed += _ => Jump();
        pa.Default.Crouch.performed += ctx => Crouch(ctx);
        pa.Default.Crouch.canceled += ctx => Crouch(ctx);
        pa.Enable();
        
    }
    private void OnDisable()
    {
        pa.Disable();
    }

    private void Jump()
    {
        Debug.Log("jump");
        if(canJump)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;
        }
    }

    private void Crouch(CallbackContext ctx)
    {
        Debug.Log("Crouch");
        if (ctx.performed)
        {

        }
        if (ctx.canceled)
        {

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
        //if hitting a collectable
        if (collision.gameObject.CompareTag("Collectable"))
        {
            ds.score += 100;
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), collision);
        }
        Destroy(collision.gameObject.transform.parent.gameObject);

    }
}
