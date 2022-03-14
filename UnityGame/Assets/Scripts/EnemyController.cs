using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    public Transform leftPoint;
    public Transform rightPoint;

    private bool movingRight;

    private Rigidbody2D RB;

    public SpriteRenderer SR;

    private Animator anim;

    public float moveTime;
    public float waitTime;

    private float moveCount;
    private float waitCount;

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        leftPoint.parent = null;
        rightPoint.parent = null;

        movingRight = true;

        moveCount = moveTime;

    }

    // Update is called once per frame
    void Update()
    {
        if (moveCount > 0)
        {
            moveCount -= Time.deltaTime;

            if (movingRight)
            {
                SR.flipX = true;
                RB.velocity = new Vector2(moveSpeed, RB.velocity.y);

                if (transform.position.x > rightPoint.position.x)
                {
                    movingRight = false;
                }
            }
            else
            {
                SR.flipX = false;
                RB.velocity = new Vector2(-moveSpeed, RB.velocity.y);

                if (transform.position.x < leftPoint.position.x)
                {
                    movingRight = true;
                }
            }

            if(moveCount <= 0)
            {
                waitCount = Random.Range(waitTime * 0.75F, waitTime * 1.25f);
            }

            anim.SetBool("isMoving", true);
        }else if(waitCount > 0)
        {
            waitCount -= Time.deltaTime;
            RB.velocity = new Vector2(0f, RB.velocity.y);

            if(waitCount <= 0)
            {
                moveCount = Random.Range(moveTime * 0.75F, moveTime * 1.25f);
            }
            anim.SetBool("isMoving", false);
        }
        
    }
}
