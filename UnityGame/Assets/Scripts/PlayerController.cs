using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public Rigidbody2D RB;

    public float moveSpeed;
    public float jumpForce;

    private bool isGrounded;
    private bool canDoubleJump;

    public Transform groundCheck;
    public LayerMask whatIsGround;

    private Animator anim;
    private SpriteRenderer SR;

    public float knockbackLength;
    public float knockbackForce;
    private float knockbackCounter;

    public float dashForce;
    public float dashDuration;

    public float bounceForce;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (knockbackCounter <= 0)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);

            if (!Input.GetButtonDown("Fire3"))
            {
                RB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), RB.velocity.y);
            }
            else
            {
                RB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal") * dashForce, RB.velocity.y);
            }

            if (isGrounded)
            {
                canDoubleJump = true;
            }

            if (Input.GetButtonDown("Jump"))
            {
                if (isGrounded)
                {
                    RB.velocity = new Vector2(RB.velocity.x, jumpForce);
                    AudioManager.instance.PlaySFX(10);
                }
                else
                {
                    if (canDoubleJump)
                    {
                        RB.velocity = new Vector2(RB.velocity.x, jumpForce);
                        canDoubleJump = false;
                        AudioManager.instance.PlaySFX(10);
                    }
                }
            }

            if (RB.velocity.x < 0)
            {
                SR.flipX = true;
            }
            else if (RB.velocity.x > 0)
            {
                SR.flipX = false;
            }
        }
        else
        {
            knockbackCounter -= Time.deltaTime;

            //determine direction of player
            if (!SR.flipX)
            {
                RB.velocity = new Vector2(-knockbackForce, 1.01f*RB.velocity.y);
            }
            else
            {
                RB.velocity = new Vector2(knockbackForce, 1.01f * RB.velocity.y);
            }
        }

        anim.SetFloat("moveSpeed", Mathf.Abs(RB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
    }

    public void Knockback()
    {
        knockbackCounter = knockbackLength;
        RB.velocity = new Vector2(0f, knockbackForce);

        anim.SetTrigger("hurt");
    }

    public void Bounce()
    {
        RB.velocity = new Vector2(RB.velocity.x, bounceForce);
        AudioManager.instance.PlaySFX(10);
    }
}
