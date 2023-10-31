using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public float jumpForce = 15.5f; 
    private bool isJumping = false; 
    // public Scanner scanner;
    // public Hand[] hands;
    public RuntimeAnimatorController[] animCon;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    private RectTransform rectTransform; 

    Animator anim;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rectTransform = GetComponent<RectTransform>();
        spriter = GetComponent<SpriteRenderer>();
        Transform playerTransform = this.transform;

        anim = playerTransform.Find("UnitRoot").GetComponent<Animator>();
        // scanner = GetComponent<Scanner>();
        // hands = GetComponentsInChildren<Hand>(true);
    }

    // void OnEnable()
    // {
    //     speed = 3;
    //     anim.runtimeAnimatorController = animCon[0];
    //     // speed *= Character.Speed;
    //     // anim.runtimeAnimatorController = animCon[GameManager.instance.playerId];
    // }
    // Update is called once per frame
    // void Update()
    // {
    //     if (!GameManager.instance.isLive)
    //         return;
    // }

    void Update()
    {
        Move();
        Jump();
    }
    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > speed) 
            rigid.velocity = new Vector2(speed, rigid.velocity.y);
        else if (rigid.velocity.x < speed * (-1)) 
            rigid.velocity = new Vector2(speed * (-1), rigid.velocity.y);

        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

     
        if (Input.GetButton("Horizontal"))
        {
            if (rigid.velocity.normalized.x > 0)
            {
                rectTransform.localRotation = Quaternion.Euler(0, 180, 0);
            } else if (rigid.velocity.normalized.x < 0)
            {
                rectTransform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }

    }
        // Vector2 nextVec = rigid.velocity * speed * Time.fixedDeltaTime;
        // rigid.MovePosition(rigid.position + rigid.velocity);
    void Jump()
    {
 
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }


        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D hit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("PlatForm"));
            if (hit.collider != null)
            {
                if (hit.distance < 0.5f)
                {
                    isJumping = false;
                }
            }
        }


    }
    // void FixedUpdate()
    // {
    //     // rigid.AddForce(inputVec); 힘주기
    //     // rigid.velocity = inputVec; 가속
    //     // if (!GameManager.instance.isLive)
    //     //     return;
    //     Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
    //     rigid.MovePosition(rigid.position + nextVec);
    // }

    // void OnMove(InputValue value)
    // {
    //     inputVec = value.Get<Vector2>();
    //     Debug.Log("hi");

    // }

    // void LateUpdate()
    // {
    //     // if (!GameManager.instance.isLive)
    //     //     return;
    //     anim.SetFloat("Speed", inputVec.magnitude);

    //     if (inputVec.x != 0) {
    //         spriter.flipX = inputVec.x < 0;

    //     }
    // }

    // void OnCollisionStay2D(Collision2D collision)
    // {
        // if (!GameManager.instance.isLive)
        //     return;
        // GameManager.instance.health -= Time.deltaTime * 10;

        // if (GameManager.instance.health < 0) {
        //     for (int index=2; index < transform.childCount; index++) {
        //         transform.GetChild(index).gameObject.SetActive(false);
        //     }

            // anim.SetTrigger("Dead");
            // GameManager.instance.GameOver();
        // }
    // }
}
