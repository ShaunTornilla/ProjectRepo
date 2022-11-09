using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{

    [SerializeField] float JumpForce, maxSpeed, knockbackForce;
    private float movement;
    private Animator an;
    private Rigidbody2D rb;
    private PlayerControls pc;
    public HealthManager healthSystem;

    private GroundChecker gr;

    public InteractChecker ic;

    private AudioSource sound;
    public AudioClip damageSound;
    public Vector2 knockbackDirection;


    private void Awake()
    {
        pc = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        gr = transform.Find("GroundCheck").GetComponent<GroundChecker>();
        ic = transform.Find("InteractCheck").GetComponentInParent<InteractChecker>();
        healthSystem = GameObject.Find("HealthSystem").GetComponent<HealthManager>();
        sound = GetComponent<AudioSource>();
        

    }

    private void OnEnable()
    {
        pc.Enable();
        pc.Default.Jump.performed += _ => Jump();
        pc.Default.Fall.performed += _ => FastFall();
        pc.Default.Interact.performed += _ => Interact();
        pc.Default.Interact.canceled += _ => StopInteract();
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

    /// <summary>
    /// 
    /// </summary>

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

    void Interact()
    {
        Debug.Log("Interact Button (E) Pressed");
        ic.pressed = true;
    }

    void StopInteract()
    {
        Debug.Log("Interact Button Released");
        ic.pressed = false;
    }

    public void Knockback()
    {
        healthSystem.health--;
        rb.AddForce(knockbackDirection * knockbackForce * rb.mass, ForceMode2D.Impulse);
    }

    private void OnBecameInvisible()
    {
        GameController.gameOver = true;
    }


}
