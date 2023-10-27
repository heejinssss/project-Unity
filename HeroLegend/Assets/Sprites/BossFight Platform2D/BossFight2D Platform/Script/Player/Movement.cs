using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Collision coll;
    [HideInInspector]
    public Rigidbody2D rb;
    private AnimationScript anim;

    [Space]
    [Header("Stats")]
    public float speed = 10;
    public float jumpForce = 50;
    
    [Space]
    [Header("Booleans")]
    public bool canMove = true;
    
    [Space]
    private bool groundTouch;

    [Header("Attack Energy")]
    public Transform  spawnEnergyLeft;
    public Transform  spawnEnergyRight;
    public GameObject energyPrefab;
    
    public int side = 1;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collision>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<AnimationScript>();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
        else if(canMove)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);

        Walk(dir);
        anim.SetHorizontalMovement(x, y, rb.velocity.y);

    if (Input.GetButtonDown("Jump"))
    {
       
        anim.SetTrigger("jump");
        if (coll.onGround)
        Jump(Vector2.up);
    }

    if (Input.GetButtonDown("Fire1") && coll.onGround)
    {
            
            anim.SetTrigger("attack");
    }
    
    if(!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }

       
    if(x > 0)
        {
            side = 1;
            anim.Flip(side);
        }
    if (x < 0)
        {
            side = -1;
            anim.Flip(side);
        }
    }
    private void Walk(Vector2 dir)
    {
        if (!canMove)
            return;
        rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        ShakeCamera.Instance.ShakeCameraOne();
    }

    private void Jump(Vector2 dir)
    {   rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;
        ShakeCamera.Instance.ShakeCameraOne();
    }

    public void EnableMove()
    {
        canMove = true;
    }
    public void DisableMove()
    {
        canMove = false;
    }

    public void InstantiateEnergy()
    {
        if(side == 1)
        {
            GameObject tempPrefab = Instantiate(energyPrefab, spawnEnergyLeft.position, spawnEnergyLeft.localRotation);
            tempPrefab.transform.localScale = new Vector3(tempPrefab.transform.localScale.x * side, tempPrefab.transform.localScale.y, tempPrefab.transform.localScale.z);
            tempPrefab.GetComponent<Rigidbody2D>().velocity = new Vector2(20 * side, 0);

            Destroy(tempPrefab, 1);
        }
        if(side == -1)
        {
            GameObject tempPrefab = Instantiate(energyPrefab, spawnEnergyRight.position, spawnEnergyRight.localRotation);
            tempPrefab.transform.localScale = new Vector3(tempPrefab.transform.localScale.x * side, tempPrefab.transform.localScale.y, tempPrefab.transform.localScale.z);
            tempPrefab.GetComponent<Rigidbody2D>().velocity = new Vector2(20 * side, 0);

            Destroy(tempPrefab, 1);
        }
    }
     
  
}
