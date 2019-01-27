using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    private float Speed = 2;
    private bool isRight = true;
    private bool isShooting = false;
    private SpriteRenderer fishSpriteRenderer;
    private Animator fishAnimator;
    private RaycastHit2D hit;
    public LayerMask groundLayer;
    public LayerMask playerLayer;
    public BulletController bullet;


    void Start()
    {
        fishSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        fishAnimator = gameObject.GetComponent<Animator>();
        fishSpriteRenderer.flipX = true;
    }

    void Update()
    {
        if(Physics2D.Raycast(transform.position, Vector2.down + Vector2.right, 10f, groundLayer).collider == null)
        {
            isRight = false;
            fishSpriteRenderer.flipX = false;
        }
        if(Physics2D.Raycast(transform.position, Vector2.down + Vector2.left, 10f, groundLayer).collider == null)
        {
            isRight = true;
            fishSpriteRenderer.flipX = true;
        }

        if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.5f), isRight ? Vector2.right : Vector2.left, 10f, playerLayer).collider?.tag == "Player")
        {
            if(!isShooting)
            {
                isShooting = true;
                StartCoroutine(Shoot());
                fishAnimator.SetInteger("FishAnim", 1);
            }
        }
        else
        {
            isShooting = false;
            fishAnimator.SetInteger("FishAnim", 0);
            transform.position = new Vector2(transform.position.x + (isRight ? Speed : -Speed) * Time.deltaTime, transform.position.y);
        }
    }

    IEnumerator Shoot()
    {
        while(isShooting)
        {
            bullet.isRight = isRight;
            Instantiate(bullet, new Vector2(transform.position.x + (isRight ? 0.5f : -0.5f), transform.position.y + 0.2f), transform.rotation);
            yield return new WaitForSeconds(1);
        }
    }

}
