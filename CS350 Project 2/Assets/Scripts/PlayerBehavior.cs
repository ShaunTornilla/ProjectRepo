using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{

    [SerializeField] float JumpForce, maxSpeed;
    private float movement;
    private Animator an;
    private Rigidbody2D rb;
    private PlayerControls pc;
    private GroundChecker gr;
    private void Awake()
    {
        pc = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        gr = transform.Find("GroundCheck").GetComponent<GroundChecker>();
    }

    private void OnEnable()
    {
        pc.Enable();
        pc.Default.Jump.performed += _ => Jump();
        pc.Default.Fall.performed += _ => FastFall();
        pc.Default.Fall.canceled += _ => StopFastFall();
    }

    private void OnDisable()
    {
        pc.Disable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        an.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        an.SetFloat("velocityY", rb.velocity.y);
        an.SetBool("grounded", gr.grounded);

        movement = pc.Default.Move.ReadValue<float>();
        Move();
        if(movement<0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if(movement>0)
        {
            transform.rotation = Quaternion.identity;
        }
    }

    void Move()
    {
        float yVel = rb.velocity.y;
        rb.velocity = new Vector2(maxSpeed * movement, yVel);
    }

    void Jump()
    {
        Debug.Log(gr.grounded);
        if (gr.grounded)
        {
            rb.AddForce(transform.up * JumpForce * rb.mass, ForceMode2D.Impulse);
        }
    }

    void FastFall()
    {
        rb.gravityScale = 1.5f;
    }

    void StopFastFall()
    {
        rb.gravityScale = 1f;
    }
}
