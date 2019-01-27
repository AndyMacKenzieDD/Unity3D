using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float Speed = 5;
    private Animator playerAnimator;
    private SpriteRenderer playerSpriteRenderer;
    private Rigidbody2D playerRigitBody;
    private Collider2D playerCollider;
    private Vector2 playerPosition;

    public LayerMask groundLayer;

    void Start()
    {
        playerAnimator = gameObject.GetComponent<Animator>();
        playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        playerRigitBody = gameObject.GetComponent<Rigidbody2D>();
        playerCollider = gameObject.GetComponent<Collider2D>();
    }

    void Update()
    {
        if(playerPosition.y > transform.position.y)
        {
            playerAnimator.SetInteger("PlayerAnim", 3);
        }

        if (IsGrounded())
        {
            playerAnimator.SetInteger("PlayerAnim", 0);
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                playerSpriteRenderer.flipX = true;
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                playerSpriteRenderer.flipX = false;
            }

            if (IsGrounded())
            {
                playerAnimator.SetInteger("PlayerAnim", 1);
            }

            transform.position = new Vector2(transform.position.x + Input.GetAxis("Horizontal") * Speed * Time.deltaTime, transform.position.y);
        }
        if (Input.GetButton("Jump") && IsGrounded())
        {
            playerAnimator.SetInteger("PlayerAnim", 2);
            playerRigitBody.velocity = new Vector2(0, 0);
            playerRigitBody.AddForce(transform.up * 5, ForceMode2D.Impulse);
        }
        playerPosition = transform.position;
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 0.0083f, groundLayer);
    }
}
