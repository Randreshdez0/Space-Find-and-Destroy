using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 100.0f;
    public float jumpForce = 350.0f;
    public float dashSpeed = 15f;
    public float airDrag = 0.8f;
    public Transform bottomTransform;

    private Rigidbody2D rb;
    private Animator animator;


    private Vector2 currentVelocity;
    private float previousPositionY;
    public bool isGrounded;
    public bool isDashing;

    public float groundCheckSize = 0.01f;

    public GameObject graphics;
    public GameObject playerTrail;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        
    }

    private void FixedUpdate()
    {
        Move();
        HandleCollisions();
        previousPositionY = transform.position.y;
    }

    private void Move()
    {
        float velocity = Input.GetAxis("Horizontal") * speed;
        bool isJumping = Input.GetKey(KeyCode.Space);

        animator.SetFloat("Speed", Mathf.Abs(velocity));
        //falling i think
        if (!isGrounded)
        {
            velocity *= airDrag;
            animator.SetBool("isGrounded", false);
        }

        // Horizontal Movement
        rb.velocity = Vector2.SmoothDamp(rb.velocity, new Vector2(velocity, rb.velocity.y), ref currentVelocity, groundCheckSize);

        // Initiate Jump
        if (isJumping)
        {
            animator.SetBool("IsJumping", true);
            isGrounded = false;
            rb.AddForce(new Vector2(0f, jumpForce));
        }

        // Cancel Jump
        if (!isGrounded && !isJumping && rb.velocity.y > 0.01f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.95f);
        }

        //flip dude
        Vector2 characterScale = graphics.transform.localScale;
        if (Input.GetAxis("Horizontal") < 0)
        {
            characterScale.x = -5;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            characterScale.x = 5;
        }
        graphics.transform.localScale = characterScale;
        //check dash
        if (Input.GetKey(KeyCode.LeftShift)){
            isDashing = true;
        }
        else isDashing = false;

        if (isDashing) Dash();
    }

    private void HandleCollisions()
    {
        bool wasOnGround = isGrounded;
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(bottomTransform.position, 0.6f);
        foreach (var collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                isGrounded = true;
                animator.SetBool("isGrounded", true);

                if (!wasOnGround && previousPositionY > transform.position.y)
                    HandleLanding();
            }
        }
    }

    private void HandleLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    private void Dash()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
                    rb.AddForce(new Vector2(-dashSpeed, 0f));
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            rb.AddForce(new Vector2(dashSpeed, 0f));
        }

    }

}