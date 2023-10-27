using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator    playerAnimator;

    //VARIABLE PUBLIC
    public bool  isGrounded;
    public float speed = 10;
    public float jumpForce = 200;
    public bool  lookLeft;

    //PRIVATE VARIABLE
    private float moveInput;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //IsGrounded?


        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if(Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector2(0, jumpForce));
        }

        //Update Animations
        if(moveInput != 0)
        {
            playerAnimator.SetBool("Walk", true);
        }
        else
        {
            playerAnimator.SetBool("Walk", false);
        }

        //Flip
        if (moveInput > 0 && lookLeft == true)
        {
            Flip();
        }
        else if (moveInput < 0 && lookLeft == false)
        {
            Flip();
        }

    }

    void Flip()
    {
        lookLeft = !lookLeft;
        float x = transform.localScale.x;
        x *= -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }
}
